using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed class GetClubDetailsQueryHandler(
        IClubQueries queries
    ) : IQueryHandler<GetClubDetailsQuery, ClubDetails?>
    {
        private readonly IClubQueries _queries = queries;

        public Task<ClubDetails?> Handle(GetClubDetailsQuery request, CancellationToken cancellationToken)
        {
            return _queries.GetClubDetailsAsync(request.ClubId, cancellationToken);
        }
    }
}
