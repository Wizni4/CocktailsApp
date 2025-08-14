

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class DeleteIngredientAuthorization
        : IAuthorize<DeleteIngredientCommand>
    {
        public Task AuthorizeAsync(DeleteIngredientCommand request, ICurrentUser user, CancellationToken cancellationToken)
        {
            if(!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
