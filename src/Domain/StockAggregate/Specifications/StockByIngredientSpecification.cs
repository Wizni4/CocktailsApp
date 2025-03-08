/*
 * Domain namespaces
 */
using Domain.Shared;
using Domain.SeedWork;
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace Domain.StockAggregate
{
    class StockByIngredientSpecification(Ingredient ingredient) : Specification<Stock>
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
