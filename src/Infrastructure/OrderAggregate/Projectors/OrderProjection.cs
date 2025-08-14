// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Orders;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.OrderAggregate
{
    public sealed class OrderProjection : IProjection
    {
        public IProjection Inner { get; }

        public OrderProjection(IMapper autoMapper)
        {
            Inner = new Projection<OrderRead>(autoMapper);
        }

        public bool CanHandle(Type eventType) => Inner.CanHandle(eventType);

        public Task HandleAsync(object @event, EFReadDbContext db, CancellationToken ct)
            => Inner.HandleAsync(@event, db, ct);
    }
}
