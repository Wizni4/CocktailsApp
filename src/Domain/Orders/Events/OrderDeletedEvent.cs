// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.Common;


namespace CocktailsApp.Domain.OrderAggregate
{
    public sealed record OrderDeletedEvent(
        Guid OrderId,
        Guid ActorId
    ) : DomainEvent(OrderId, typeof(Order), ActorId);
}
