/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.IngredientPricingAggregate
{
    public class IngredientPricingUpdatedEvent : DomainEvent
    {
        public Guid IngredientId { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }
        internal IngredientPricingUpdatedEvent(Guid ingredientId, decimal oldPrice, decimal newPrice)
        {
            IngredientId = ingredientId;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }
}
