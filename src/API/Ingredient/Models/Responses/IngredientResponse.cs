// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.SeedWork;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Ingredient
{
    public class IngredientResponse : ImageResponse
    {
        public required Guid Id { get; set; }
        public List<string>? Allergens { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required bool IsAlcoholic { get; set; }
    }
}
