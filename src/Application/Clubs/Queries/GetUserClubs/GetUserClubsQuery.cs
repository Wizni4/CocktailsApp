

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed record GetUserClubsQuery(
        Guid UserId
    ) : IQuery<UserClubs?>, ICacheableQuery
    {
        public string CacheKey => $"GetUserClubs:{UserId}";
        public TimeSpan Ttl => TimeSpan.FromHours(8);
    }
}
