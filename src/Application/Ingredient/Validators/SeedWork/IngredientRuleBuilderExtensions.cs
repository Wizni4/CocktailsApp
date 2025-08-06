// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using FluentValidation;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Ingredient
{
    public static class IngredientRuleBuilderExtensions
    {
        public static IRuleBuilder<T, Guid> IsIngredientExists<T>(this IRuleBuilder<T, Guid> ruleBuilder, IRepository<DomainIngredient> ingredientRepository)
        {
            return ruleBuilder.SetValidator(new IngredientExistsValidator(ingredientRepository));
        }
    }
}
