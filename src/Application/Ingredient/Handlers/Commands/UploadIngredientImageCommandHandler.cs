// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

using MediatR;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Ingredient
{
    public class UploadIngredientImageCommandHandler(
        IImageService imageService,
        IIngredientRepository ingredientRepository,
        IUnitOfWork unitOfWork
    ) : ICommandHandler<UploadIngredientImageCommand, string>
    {
        private readonly IImageService _imageService = imageService;
        private readonly IIngredientRepository _ingredientRepository = ingredientRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<string> Handle(UploadIngredientImageCommand request, CancellationToken cancellationToken)
        {
            var ingredientResult = _ingredientRepository.ReadAsync(
                new IngredientByIdCommandSpecification(request.IngredientId),
                cancellationToken);

            var imageIdResult = _imageService.UploadImageAsync(
                request.Image,
                request.ImageName);

            await Task.WhenAll(ingredientResult, imageIdResult);

            var ingredient = await ingredientResult;
            var imageId = await imageIdResult;

            ingredient!.UpdateImage(imageId, request.ActorId);

            await _ingredientRepository.UpdateAsync(ingredient, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return imageId;
        }
    }
}
