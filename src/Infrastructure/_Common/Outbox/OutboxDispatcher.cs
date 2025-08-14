// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Infrastructure.Persistence;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CocktailsApp.Infrastructure.Common
{
    public sealed class OutboxDispatcher(
        IServiceScopeFactory scopeFactory
    ) : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private static readonly JsonSerializerSettings s_json = new()
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy() // match whatever you use elsewhere
            }
        };
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
                        // Resolve the event type directly from the saved string
                        var type = Type.GetType(msg.Type, throwOnError: false);
                        if (type is null) { msg.ProcessedOn = DateTimeOffset.UtcNow; continue; }

                        var evt = (INotification?)JsonConvert.DeserializeObject(msg.Payload, type, s_json);
                        if (evt is null) { msg.ProcessedOn = DateTimeOffset.UtcNow; continue; }

                        await mediator.Publish(evt, cancellationToken);

                        msg.ProcessedOn = DateTimeOffset.UtcNow;
                        msg.AttemptCount++;
                    }
                    catch
                    {
                        msg.AttemptCount++; // will retry later
                    }
                }

                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
