/*
 * Domain namespaces
 */
using Domain.SeedWork;
using Domain.Shared;


/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace Domain.CocktailAggregate
{
    public class CocktailByIngredientsSpecification(List<Ingredient> ingredients) : Specification<Cocktail>
    {
        private readonly IEnumerable<Ingredient> _ingredients = ingredients;

        public override Expression<Func<Cocktail, bool>> SpecExpression
        {
            get
            {
                return cocktail => _ingredients.All(ingredient => cocktail.Ingredients.Any(ci => ci.Ingredient.Name == ingredient.Name));
            }
        }
    }
}
