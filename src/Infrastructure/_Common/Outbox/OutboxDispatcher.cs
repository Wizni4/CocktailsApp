// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Infrastructure.Persistence;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CocktailsApp.Infrastructure.Common
{
    public sealed class OutboxDispatcher(
        IKafkaProducer producer,
        IServiceScopeFactory scopeFactory
    ) : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly IKafkaProducer _producer = producer;
        private const int BatchSize = 200;
        private static readonly TimeSpan s_idleDelay = TimeSpan.FromMilliseconds(250);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<EFWriteDbContext>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var batch = await dbContext.Set<OutboxMessage>()
                    .Where(m => m.ProcessedOn == null)
                    .OrderBy(m => m.OccurredOn).ThenBy(m => m.Id)
                    .Take(BatchSize)
                    .ToListAsync(cancellationToken);

                if (batch.Count == 0) { await Task.Delay(s_idleDelay, cancellationToken); continue; }

                foreach (var msg in batch)
                {
                    try
                    {
                        var headers = new[]
                        {
                            new KeyValuePair<string,string>("type", msg.Type),
                            new KeyValuePair<string,string>("eventId", msg.Id.ToString()),
                        };

                        await _producer.ProduceAsync(msg.AggregateId.ToString(), msg.Payload, headers, cancellationToken);

                        msg.ProcessedOn = DateTimeOffset.UtcNow;
                        msg.AttemptCount++;
                    }
                    catch
                    {
                        msg.AttemptCount++;
                    }

                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
