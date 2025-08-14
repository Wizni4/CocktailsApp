
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class GetIngredientDetailsAuthorization
        : IAuthorize<GetIngredientDetailsQuery>
    {
        public Task AuthorizeAsync(GetIngredientDetailsQuery request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
