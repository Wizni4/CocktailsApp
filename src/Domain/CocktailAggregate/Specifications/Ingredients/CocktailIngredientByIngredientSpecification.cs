/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using System.Linq.Expressions;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class CocktailIngredientByIngredientSpecification(Guid ingredientId) : Specification<CocktailIngredient>
    {
        private readonly Guid _ingredientId = ingredientId;

        public override Expression<Func<CocktailIngredient, bool>> SpecExpression => ci => ci.IngredientId == _ingredientId;
    }
}
