/*
 * Domain namespaces
 */
using Domain.SeedWork;
using Domain.Shared;

/*
 * Framework namespaces
 */

namespace Domain.StockAggregate
{
    public class IngredientOutOfStock : DomainEvent
    {
        public Guid StockId { get; }
        public Ingredient Ingredient { get; }
        internal IngredientOutOfStock(Guid stockId, Ingredient ingredient)
        {
            StockId = stockId;
            Ingredient = ingredient;
        }
    }
}
