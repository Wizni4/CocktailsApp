// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.User;

using FluentValidation;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;
using DomainUser = CocktailsApp.Domain.UserAggregate.User;

namespace CocktailsApp.Application.Cocktail
{
    public class CreateCocktailCommandValidator : AbstractValidator<CreateCocktailCommand>
    {
        public CreateCocktailCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.CreatorId)
                .ValidGuid()
                .IsUserExists(unitOfWork.Set<DomainUser>());
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
                        .ValidGuid()
                        .IsIngredientExists(unitOfWork.Set<DomainIngredient>());
                    a.RuleFor(i => i.Quantity)
                        .GreaterThan(0);
                    a.RuleFor(i => i.Unit)
                        .ValidEnum();
                });
            RuleFor(c => c.Name)
                .ValidString();
        }
    }
}
