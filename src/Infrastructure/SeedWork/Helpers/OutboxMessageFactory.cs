// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Domain.SeedWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;



namespace CocktailsApp.Infrastructure.SeedWork
{
    public static class OutboxMessageFactory
    {
        private static readonly JsonSerializerSettings s_json = new()
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy() // match whatever you use elsewhere
            }
        };

        public static OutboxMessage FromDomainEvent(IDomainEvent e)
        {
            var t = e.GetType();
            return new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = (e as DomainEvent)?.Created ?? DateTimeOffset.UtcNow,
                AggregateType = $"{e.AggregateType.FullName}, {e.AggregateType.Assembly.GetName().Name}",
                AggregateId = e.AggregateId,
                AggregateVersion = null,
                ActorId = e.ActorId,
                Type = $"{t.FullName}, {t.Assembly.GetName().Name}",
                Payload = JsonConvert.SerializeObject(e, s_json),
                AttemptCount = 0
            };
        }
    }

}
