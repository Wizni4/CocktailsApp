

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class UploadIngredientImageAuthorization
        : IAuthorize<UploadIngredientImageCommand>
    {
        public Task AuthorizeAsync(UploadIngredientImageCommand request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
