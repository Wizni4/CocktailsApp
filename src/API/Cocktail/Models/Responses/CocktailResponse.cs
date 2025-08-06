// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.Ingredient;

namespace CocktailsApp.API.Cocktail
{
    public class CocktailResponse
    {
        public required Guid Id { get; set; }
        public string? Description { get; set; }
        public required List<CocktailIngredientResponse> Ingredients { get; set; }
        public required string Name { get; set; }
    }
}
