// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Cocktail
{
    public class CocktailIngredientRead : ReadEntity
    {
        public Guid CocktailIngredientId { get; set; }
        public Guid IngredientId { get; set; }
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool IsAlcoholic { get; set; } = default;
        public string? ImageId { get; set; } = null;
        public List<string>? Allergens { get; set; } = null;
        public decimal Quantity { get; set; } = default;
        public string Unit { get; set; } = default!;
    }
}
