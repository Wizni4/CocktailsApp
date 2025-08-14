// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.Common;


namespace CocktailsApp.Domain.Prices
{
    public sealed record IngredientPricingDeletedEvent(
        Guid IngredientPricingId,
        Guid ActorId
    ) : DomainEvent(IngredientPricingId, typeof(Pricing), ActorId);
}
