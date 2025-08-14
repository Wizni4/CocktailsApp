

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed record GetCocktailListItemQuery(
        Guid CocktailId
    ) : IQuery<CocktailListItem?>, ICacheableQuery
    {
        public string CacheKey => $"GetCocktailListItem:{CocktailId}";
        public TimeSpan Ttl => TimeSpan.FromHours(8);
    }
}
