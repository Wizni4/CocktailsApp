

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class CreateIngredientAuthorization
        : IAuthorize<CreateIngredientCommand>
    {
        public Task AuthorizeAsync(CreateIngredientCommand request, ICurrentUser user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
