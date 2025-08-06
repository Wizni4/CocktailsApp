// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.User;

using FluentValidation;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;
using DomainUser = CocktailsApp.Domain.UserAggregate.User;


namespace CocktailsApp.Application.Ingredient
{
    public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.CreatorId)
                .ValidGuid()
                .IsUserExists(unitOfWork.Set<DomainUser>());
            RuleFor(c => c.Name)
                .MustAsync(async (name, _) =>
                {
                    var ingredient = await unitOfWork.Set<DomainIngredient>().ReadAsync(new IngredientByNameSpecification(name!));
                    return ingredient is null;
                }).WithMessage(c => $"Ingredient: '{c.Name}' already exists.");
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
