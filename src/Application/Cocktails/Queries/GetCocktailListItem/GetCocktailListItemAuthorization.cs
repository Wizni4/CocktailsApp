
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed class GetCocktailListItemAuthorization
        : IAuthorize<GetCocktailListItemQuery>
    {
        public Task AuthorizeAsync(GetCocktailListItemQuery request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
