
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class GetIngredientTypesAuthorization
        : IAuthorize<GetIngredientTypesQuery>
    {
        public Task AuthorizeAsync(GetIngredientTypesQuery request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
