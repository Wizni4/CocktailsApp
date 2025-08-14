

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed class GetUserClubsQueryHandler(
        IClubQueries queries
    ) : IQueryHandler<GetUserClubsQuery, UserClubs?>
    {
        private readonly IClubQueries _queries = queries;
        public Task<UserClubs?> Handle(GetUserClubsQuery request, CancellationToken cancellationToken)
        {
            return _queries.GetUserClubsAsync(request.UserId, cancellationToken);
        }
    }
}
