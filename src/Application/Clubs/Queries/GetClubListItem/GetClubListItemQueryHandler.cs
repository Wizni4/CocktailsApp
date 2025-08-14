

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed class GetClubListItemQueryHandler(
        IClubQueries queries
    ) : IQueryHandler<GetClubListItemQuery, ClubListItem?>
    {
        private readonly IClubQueries _queries = queries;

        public Task<ClubListItem?> Handle(GetClubListItemQuery request, CancellationToken cancellationToken)
        {
            return _queries.GetClubListItemAsync(request.ClubId, cancellationToken);
        }
    }
}
