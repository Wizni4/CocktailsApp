using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed record GetCocktailDetailsQuery(
        Guid CocktailId
    ) : IQuery<CocktailDetails?>, ICacheableQuery
    {
        public string CacheKey => $"GetCocktailDetails:{CocktailId}";
        public TimeSpan Ttl => TimeSpan.FromHours(8);
    }
}
