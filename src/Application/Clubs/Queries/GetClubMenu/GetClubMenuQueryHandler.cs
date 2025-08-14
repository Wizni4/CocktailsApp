

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed class GetClubMenuQueryHandler(
        IClubQueries queries
    ) : IQueryHandler<GetClubMenuQuery, ClubMenu?>
    {
        private readonly IClubQueries _queries = queries;
        public Task<ClubMenu?> Handle(GetClubMenuQuery request, CancellationToken cancellationToken)
        {
            return _queries.GetClubMenuAsync(request.ClubId, cancellationToken);
        }
    }
}
