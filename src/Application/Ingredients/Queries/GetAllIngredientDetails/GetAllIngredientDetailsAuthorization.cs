
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class GetAllIngredientDetailsAuthorization
        : IAuthorize<GetAllIngredientDetailsQuery>
    {
        public Task AuthorizeAsync(GetAllIngredientDetailsQuery request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
