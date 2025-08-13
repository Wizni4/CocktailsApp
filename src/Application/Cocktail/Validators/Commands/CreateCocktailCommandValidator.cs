// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using FluentValidation;

namespace CocktailsApp.Application.Cocktail
{
    public class CreateCocktailCommandValidator : CommandValidator<CreateCocktailCommand>
    {
        public CreateCocktailCommandValidator()
        {
            When(c => c.Description is not null, () =>
            {
                RuleFor(c => c.Description)
                    .ValidString();

            });
            RuleFor(c => c.Ingredients)
                .ValidList();
            RuleForEach(c => c.Ingredients)
                .ChildRules(a =>
                {
                    a.RuleFor(i => i.Id)
                        .ValidGuid();
                    a.RuleFor(i => i.Quantity)
                        .GreaterThan(0);
                    a.RuleFor(i => i.Unit)
                        .ValidEnum();
                });
        }
    }
}
