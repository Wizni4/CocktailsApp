// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Ingredient;

using DomainCocktailIngredient = CocktailsApp.Domain.CocktailAggregate.CocktailIngredient;


namespace CocktailsApp.Application.Cocktail
{
    public class IngredientResolver : IValueResolver<DomainCocktailIngredient, CocktailIngredientDTO, IngredientDTO>
    {
        public IngredientDTO Resolve(
            DomainCocktailIngredient source,
            CocktailIngredientDTO destination,
            IngredientDTO destMember,
            ResolutionContext context)
        {
            if (context.Items.TryGetValue("Ingredients", out var ingredientObj)
                && ingredientObj is List<IngredientDTO> ingredients)
                return ingredients.First(i => i.Id == source.IngredientId);

            return null!;
        }
    }
}
