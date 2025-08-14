

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed record UploadIngredientImageCommand(
        Guid IngredientId,
        Stream Image,
        string ImageName
    ) : ICommand<string>, IIdempotentCommand;
}
