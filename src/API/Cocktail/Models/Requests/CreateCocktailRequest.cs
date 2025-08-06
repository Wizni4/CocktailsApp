// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CocktailsApp.API.Cocktail
{
    public class CreateCocktailRequest
    {
        public string? Description { get; set; }
        public required List<IngredientModelRequest> Ingredients { get; set; }
        public required string Name { get; set; }
    }

    public class IngredientModelRequest
    {
        public required Guid Id { get; set; }
        public required decimal Quantity { get; set; }
        public required string Unit { get; set; }
    }
}
