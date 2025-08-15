
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Ingredients;
using CocktailsApp.Infrastructure.Common;

namespace CocktailsApp.Infrastructure.Ingredients
{
    public sealed class IngredientIncludesService : IIncludesService<Ingredient>
    {
        public Func<IIncludable<Ingredient>, IIncludable> GetIncludes(IEnumerable<ILoad<Ingredient>> graph)
        {
            throw new NotImplementedException();
        }
    }
}
