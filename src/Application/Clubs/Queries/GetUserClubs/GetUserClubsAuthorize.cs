

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed class GetUserClubsAuthorize
        : IAuthorize<GetUserClubsQuery>
    {
        public Task AuthorizeAsync(GetUserClubsQuery request, ICurrentUser user, CancellationToken cancellationToken)
        {
            if(!user.IsAuthenticated) throw new UnauthorizedAccessException();
            return Task.CompletedTask;
        }
    }
}
