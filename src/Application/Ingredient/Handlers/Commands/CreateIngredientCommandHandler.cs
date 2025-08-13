// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.IngredientAggregate;


namespace CocktailsApp.Application.Ingredient
{
    public class CreateIngredientCommandHandler(
        IIngredientRepository ingredientRepository,
        IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateIngredientCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IIngredientRepository _ingredientRepository = ingredientRepository;
        public async Task<Guid> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientBuilder = new IngredientBuilder()
                .WithCreatorId(request.ActorId)
                .WithName(request.Name)
                .WithType(request.Type);

            if (request.IsAlcoholic != null && request.IsAlcoholic == true)
                ingredientBuilder.AsAlcoholic();

            if (request.Allergens is not null)
                foreach (var allergen in request.Allergens)
                    ingredientBuilder.AddAllergen(allergen);

            var ingredient = ingredientBuilder.Build();

            await _ingredientRepository.CreateAsync(ingredient, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ingredient.Id;
        }
    }
}
