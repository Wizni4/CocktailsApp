// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.SeedWork;

using MediatR;


namespace CocktailsApp.Application.Ingredient
{
    public class DeleteIngredientCommandHandler(
        IIngredientRepository ingredientRepository,
        IUnitOfWork unitOfWork
    ) : ICommandHandler<DeleteIngredientCommand, Unit>
    {
        private readonly IIngredientRepository _ingredientRepository = ingredientRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.ReadAsync(
                new IngredientByIdCommandSpecification(request.Id),
                cancellationToken);

            await _ingredientRepository.DeleteAsync(ingredient!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
