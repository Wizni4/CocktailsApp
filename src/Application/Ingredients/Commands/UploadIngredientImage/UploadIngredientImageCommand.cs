

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed record UploadIngredientImageCommand(
        Guid IngredientId,
        Stream Image,
        string ImageName,
        Guid RequestId
    ) : ICommand<string>, IIdempotentCommand
    {
        public string IdempotencyKey => $"UploadIngredientImage:{RequestId}";
    }
}
