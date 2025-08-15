// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.Common;
using CocktailsApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore.Diagnostics;

using Newtonsoft.Json;


namespace CocktailsApp.Infrastructure.Common
{
    public sealed class OutboxInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var ctx = (EFWriteDbContext)eventData.Context!;
            var entries = ctx.ChangeTracker
                .Entries<AggregateRoot>()
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var @event in entries)
            {
                var t = @event.GetType();
                var payload = JsonConvert.SerializeObject(@event);
                ctx.Set<OutboxMessage>().Add(new OutboxMessage
                {
                    OccurredOn = @event.Created,
                    AggregateType = @event.AggregateType.FullName!,
                    AggregateId = @event.AggregateId,
                    AggregateVersion = null,
                    ActorId = @event.ActorId,
                    Type = t.FullName!,
                    Payload = payload
                });
            }

            foreach (var entity in ctx.ChangeTracker.Entries<AggregateRoot>())
                entity.Entity.ClearDomainEvents();

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
