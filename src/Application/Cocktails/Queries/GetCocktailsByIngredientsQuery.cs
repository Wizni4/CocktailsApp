
/*
 * Application namespaces
 */
using Application.SeedWork;
/*
 * Framework namespaces
 */

/*
 * Domain namespaces
 */
using Domain.CocktailAggregate;
using Domain.SeedWork;
using Domain.Shared;

namespace Application.Cocktails
{
    public class GetCocktailsByIngredientsQuery(List<Ingredient> ingredients) : IQuery<Cocktail>
    {
        private readonly IEnumerable<Ingredient> _ingredients = ingredients;
        public ISpecification<Cocktail> Specification { get { return new CocktailByIngredientsSpecification([.. _ingredients]); } }

        public Func<IIncludable<Cocktail>, IIncludable>? Include { get { return opt => opt.Include(c => c.Ingredients).ThenInclude(ci => ci.Ingredient); } }
    }
}
