

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed record GetUserClubsQuery
        : IQuery<UserClubs?>, IActorScopedCache
    {
        public string CacheKey => $"GetUserClubs";
        public TimeSpan Ttl => TimeSpan.FromHours(8);
    }
}
