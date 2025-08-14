
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed record GetIngredientDetailsQuery(
        Guid IngredientId
    ) : IQuery<IngredientDetails?>, ICacheableQuery
    {
        public string CacheKey => $"GetIngredientDetails:{IngredientId}";
        public TimeSpan Ttl => TimeSpan.FromHours(8);
    }
}
