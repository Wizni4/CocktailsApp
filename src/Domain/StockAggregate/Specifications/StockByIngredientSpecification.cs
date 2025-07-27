/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace CocktailsApp.Domain.StockAggregate
{
    public class StockByIngredientSpecification(Ingredient ingredient) : Specification<Stock>
    {
        private readonly Ingredient _ingredient = ingredient;

        public override Expression<Func<Stock, bool>> SpecExpression
        {
            get
            {
                return stock => stock.Ingredient == _ingredient;
            }
        }
    }
}
