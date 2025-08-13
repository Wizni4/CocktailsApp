// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    public class ClubCocktailRead : ReadEntity
    {
        public Guid ClubCocktailId { get; set; }
        public Guid CocktailId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = null;
        public string? ImageId { get; set; } = null;
        public List<ClubCocktailIngredientRead> Ingredients { get; set; } = [];
    }
}
