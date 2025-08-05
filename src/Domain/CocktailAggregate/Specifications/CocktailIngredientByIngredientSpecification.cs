/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

using System.Linq.Expressions;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class CocktailIngredientByIngredientSpecification(Ingredient ingredient) : Specification<CocktailIngredient>
    {
        private readonly Ingredient _ingredient = ingredient;

        public override Expression<Func<CocktailIngredient, bool>> SpecExpression => ci => ci.Ingredient == _ingredient;
    }
}
