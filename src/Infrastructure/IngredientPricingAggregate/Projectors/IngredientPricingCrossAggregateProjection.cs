// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.IngredientPricing;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.IngredientPricingAggregate
{
    public sealed class IngredientPricingCrossAggregateProjection : IProjection
    {
        public IProjection Inner { get; }

        public IngredientPricingCrossAggregateProjection(IMapper autoMapper)
        {
            Inner = new Projection<IngredientPricingRead>(autoMapper);
        }

        public bool CanHandle(Type eventType) => Inner.CanHandle(eventType);

        public Task HandleAsync(object @event, EFReadDbContext db, CancellationToken ct)
            => Inner.HandleAsync(@event, db, ct);
    }
}
