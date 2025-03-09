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
        public Ingredient Ingredient { get; }
        internal IngredientOutOfStock(Guid stockId, Ingredient ingredient)
        {
            StockId = stockId;
            Ingredient = ingredient;
        }
    }
}
