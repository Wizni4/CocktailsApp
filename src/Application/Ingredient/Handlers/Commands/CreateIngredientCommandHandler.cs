// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
using CocktailsApp.Domain.IngredientAggregate;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Ingredient
{
    public class CreateIngredientCommandHandler(
        IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateIngredientCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Guid> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientBuilder = new IngredientBuilder()
                .WithCreatorId(request.CreatorId)
                .WithName(request.Name)
                .WithType(request.Type);

            if (request.IsAlcoholic != null && request.IsAlcoholic == true)
                ingredientBuilder.AsAlcoholic();

            if (request.Allergens is not null)
                foreach (var allergen in request.Allergens)
                    ingredientBuilder.AddAllergen(allergen);

            var ingredient = ingredientBuilder.Build();

            _unitOfWork.Set<DomainIngredient>().Create(ingredient);
            await _unitOfWork.SaveChangesAsync();

            return ingredient.Id;
        }
    }
}
