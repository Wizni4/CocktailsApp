using CocktailsApp.ReadStore.Common;
using CocktailsApp.ReadStore.Persistence;

using Confluent.Kafka;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Text;

namespace CocktailsApp.ReadStore.Projections
{
    public sealed class KafkaProjectionConsumer(
        IOptionsMonitor<KafkaOptions> options,        // swap to Monitor so we can read current values
        ILogger<KafkaProjectionConsumer> logger,
        IServiceScopeFactory scopeFactory
    ) : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly ILogger<KafkaProjectionConsumer> _logger = logger;
        private readonly IOptionsMonitor<KafkaOptions> _options = options;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            // Outer loop: recreate the consumer on failure and keep the web host alive
            while (!stoppingToken.IsCancellationRequested)
            {
                IConsumer<string, string>? consumer = null;

                try
                {
                    var cfg = _options.CurrentValue; // NOT null — you said they're set
                    var config = new ConsumerConfig
                    {
                        BootstrapServers = cfg.BootstrapServers,
                        GroupId = cfg.GroupId,
                        EnableAutoCommit = false,
                        AutoOffsetReset = AutoOffsetReset.Earliest,
                        BrokerAddressFamily = BrokerAddressFamily.V4,
                    };

                    consumer = new ConsumerBuilder<string, string>(config)
                        .SetErrorHandler((_, e) => _logger.LogError("Kafka error (fatal={Fatal}): {Reason}", e.IsFatal, e.Reason))
                        .SetLogHandler((_, m) => _logger.LogInformation("rdkafka[{Facility}] {Message}", m.Facility, m.Message))
                        .SetPartitionsRevokedHandler((_, parts) => _logger.LogInformation("Revoked: {Parts}", string.Join(",", parts)))
                        .Build();

                    consumer.Subscribe(cfg.Topic);

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var cr = consumer.Consume(stoppingToken); // blocking, but on background thread

                        var type = GetHeader(cr.Message.Headers, "type");
                        var eventIdS = GetHeader(cr.Message.Headers, "eventId");

                        if (type is null || eventIdS is null)
                        {
                            _logger.LogWarning("Skipping message missing headers at {Topic}/{Partition}@{Offset}",
                                cr.Topic, cr.Partition.Value, cr.Offset.Value);
                            consumer.Commit(cr);
                            continue;
                        }

                        var eventId = Guid.Parse(eventIdS);

                        using var scope = _scopeFactory.CreateScope();
                        var dispatcher = scope.ServiceProvider.GetRequiredService<ProjectionDispatcher>(); // your dispatcher
                        var db = scope.ServiceProvider.GetRequiredService<EFReadDbContext>();

                        // idempotency
                        if (await db.Set<ProcessedEvent>().AnyAsync(x => x.EventId == eventId, stoppingToken))
                        {
                            consumer.Commit(cr);
                            continue;
                        }

                        using var tx = await db.Database.BeginTransactionAsync(stoppingToken);

                        await dispatcher.DispatchAsync(type, cr.Message.Value, stoppingToken); // uses your dispatcher
                        db.Add(new ProcessedEvent
                        {
                            EventId = eventId,
                            Topic = cr.Topic,
                            Partition = cr.Partition.Value,
                            Offset = cr.Offset.Value,
                            ProcessedOnUtc = DateTime.UtcNow
                        });

                        await db.SaveChangesAsync(stoppingToken);
                        await tx.CommitAsync(stoppingToken);

                        consumer.Commit(cr);
                    }
                }
                catch (OperationCanceledException) { /* shutdown */ }
                catch (Exception ex)
                {
                    // CRITICAL: don't crash the host — log and retry
                    _logger.LogError(ex, "Kafka consumer crashed; retrying in 5s");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                finally
                {
                    try { consumer?.Close(); } catch { }
                    try { consumer?.Dispose(); } catch { }
                }
            }
        }

        private static string? GetHeader(Headers headers, string key)
        {
            if (!headers.TryGetLastBytes(key, out var bytes) || bytes is null) return null;
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
