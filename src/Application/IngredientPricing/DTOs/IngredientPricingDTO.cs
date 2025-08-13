// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.IngredientPricing
{
    public sealed class IngredientPricingDTO : EntityDTO
    {
        public Guid Id { get; set; } = default;
        public decimal Cost { get; set; } = default;
        public decimal Price { get; set; } = default;
        public decimal Margin { get; set; } = default;
        public IngredientDTO Ingredient { get; set; } = default!;
    }
}
