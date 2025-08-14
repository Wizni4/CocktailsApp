// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CocktailsApp.API.Cocktails
{
    public sealed record CocktailListItemResponse(
        Guid CocktailId,
        string Name,
        string? Description,
        string? ImageUrl,
        bool ContainsAlcohol,
        List<string> Allergens,
        int IngredientCount
    );
}
