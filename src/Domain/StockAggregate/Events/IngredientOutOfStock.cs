/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.StockAggregate
{
    public class IngredientOutOfStock : DomainEvent
    {
        public Guid StockId { get; }
        public Guid IngredientId { get; }
        internal IngredientOutOfStock(Guid stockId, Guid ingredientId)
        {
            StockId = stockId;
            IngredientId = ingredientId;
        }
    }
}
