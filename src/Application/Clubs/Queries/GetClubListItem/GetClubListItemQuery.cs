
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed record GetClubListItemQuery(
        Guid ClubId
    ) : IQuery<ClubListItem?>, ICacheableQuery
    {
        public string CacheKey => $"GetClubList:{ClubId}";
        public TimeSpan Ttl => TimeSpan.FromHours(8);
    }
}
