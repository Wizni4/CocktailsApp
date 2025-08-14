

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class UploadIngredientImageAuthorization
        : IAuthorize<UploadIngredientImageCommand>
    {
        public Task AuthorizeAsync(UploadIngredientImageCommand request, ICurrentUser user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
