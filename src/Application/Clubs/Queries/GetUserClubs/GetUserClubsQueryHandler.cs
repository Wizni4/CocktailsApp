

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed class GetUserClubsQueryHandler(
        ICurrentUserService user,
        IClubQueries queries
    ) : IQueryHandler<GetUserClubsQuery, UserClubs?>
    {
        private readonly ICurrentUserService _user = user;
        private readonly IClubQueries _queries = queries;
        public Task<UserClubs?> Handle(GetUserClubsQuery request, CancellationToken cancellationToken)
        {
            return _queries.GetUserClubsAsync(_user.UserId, cancellationToken);
        }
    }
}
