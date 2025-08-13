// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;


namespace CocktailsApp.Domain.CocktailAggregate
{
    public sealed record IngredientAddedEvent(
        Guid CocktailId,
        Guid CocktailIngredientId,
        Guid IngredientId,
        decimal Quantity,
        UnitOfMeasure Unit,
        Guid ActorId
    ) : DomainEvent(CocktailId, typeof(Cocktail), ActorId);
}
