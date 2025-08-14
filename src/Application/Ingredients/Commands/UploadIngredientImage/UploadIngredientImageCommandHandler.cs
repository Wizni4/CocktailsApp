
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public class UploadIngredientImageCommandHandler(
        ICurrentUser user,
        IImageService imageService,
        IIngredientRepository ingredientRepository
    ) : ICommandHandler<UploadIngredientImageCommand, string>
    {
        private readonly ICurrentUser _user = user;
        private readonly IImageService _imageService = imageService;
        private readonly IIngredientRepository _ingredientRepository = ingredientRepository;

        public async Task<string> Handle(UploadIngredientImageCommand request, CancellationToken cancellationToken)
        {
            var ingredientResult = _ingredientRepository.ReadAsync(
                new LoadIngredientCommandSpecification(request.IngredientId),
                cancellationToken);

            var imageIdResult = _imageService.UploadImageAsync(
                request.Image,
                request.ImageName);

            await Task.WhenAll(ingredientResult, imageIdResult);

            var ingredient = await ingredientResult;
            var imageId = await imageIdResult;

            ingredient!.UpdateImage(imageId, _user.UserId);

            await _ingredientRepository.UpdateAsync(ingredient, cancellationToken);
            return imageId;
        }
    }
}
