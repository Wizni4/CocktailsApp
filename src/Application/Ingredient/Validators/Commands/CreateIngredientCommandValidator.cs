// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using FluentValidation;


namespace CocktailsApp.Application.Ingredient
{
    public class CreateIngredientCommandValidator : CommandValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator()
        {
            RuleFor(c => c.Name)
                .ValidString();
            RuleFor(c => c.Type)
                .ValidEnum();
            RuleFor(c => c.IsAlcoholic)
                .NotNull();
            When(c => c.Allergens is not null, () =>
            {
                RuleFor(c => c.Allergens!).ValidList();
                RuleForEach(c => c.Allergens!).ValidString();
            });
        }
    }
}
