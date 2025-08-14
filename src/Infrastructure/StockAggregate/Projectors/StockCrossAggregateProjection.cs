// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Stocks;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.StockAggregate
{
    public sealed class StockCrossAggregateProjection : IProjection
    {
        public IProjection Inner { get; }

        public StockCrossAggregateProjection(IMapper autoMapper)
        {
            Inner = new Projection<StockRead>(autoMapper);
        }

        public bool CanHandle(Type eventType) => Inner.CanHandle(eventType);

        public Task HandleAsync(object @event, EFReadDbContext db, CancellationToken ct)
            => Inner.HandleAsync(@event, db, ct);
    }
}
