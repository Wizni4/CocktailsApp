// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.Ingredient;

namespace CocktailsApp.API.Cocktail
{
    public class CocktailIngredientResponse
    {
        public required Guid Id { get; set; }
        public required IngredientResponse Ingredient { get; set; }
        public required decimal Quantity { get; set; }
        public required string Unit { get; set; }
    }
}
