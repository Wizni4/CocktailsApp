
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed record GetClubMenuQuery(
        Guid ClubId
    ) : IQuery<ClubMenu?>, ICacheableQuery
    {
        public string CacheKey => $"GetClubMenu:{ClubId}";
        public TimeSpan Ttl => TimeSpan.FromHours(8);
    }
}
