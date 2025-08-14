// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.Common;

using MediatR;


namespace CocktailsApp.Infrastructure.SeedWork
{
    public class Projector<TEvent>(
        ProjectionBus bus
    ) : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
        private readonly ProjectionBus _bus = bus;

        public Task Handle(TEvent notification, CancellationToken ct)
        {
            return _bus.DispatchAsync(notification, ct);  // one place routes to all projection
        }
    }
}
