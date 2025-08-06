// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.SeedWork;

using MediatR;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Ingredient
{
    public class DeleteIngredientCommandHandler(
        IUnitOfWork unitOfWork
    ) : ICommandHandler<DeleteIngredientCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _unitOfWork.Set<DomainIngredient>().ReadAsync(
                new IngredientByIdSpecification(request.Id));

            _unitOfWork.Set<DomainIngredient>().Delete(ingredient!);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
