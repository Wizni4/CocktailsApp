// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.Common;


namespace CocktailsApp.Domain.Cocktails
{
    public sealed record IngredientUnitChangedEvent(
        Guid CocktailId,
        Guid CocktailIngredientId,
        UnitOfMeasure Unit,
        Guid ActorId
    ) : DomainEvent(CocktailId, typeof(Cocktail), ActorId);
}
