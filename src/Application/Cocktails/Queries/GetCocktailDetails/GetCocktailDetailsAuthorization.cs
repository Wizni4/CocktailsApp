using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed class GetCocktailDetailsAuthorization
        : IAuthorize<GetClubDetailsQuery>
    {
        public Task AuthorizeAsync(GetClubDetailsQuery request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
