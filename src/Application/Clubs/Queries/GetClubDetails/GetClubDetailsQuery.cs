using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed record GetClubDetailsQuery(
        Guid ClubId
    ) : IQuery<ClubDetails?>, ICacheableQuery
    {
        public string CacheKey => $"GetClubDetailsQuery:{ClubId}";
        public TimeSpan Ttl => TimeSpan.FromHours(8);
    }
}
