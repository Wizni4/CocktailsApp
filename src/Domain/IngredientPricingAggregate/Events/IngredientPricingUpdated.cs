/*
 * Domain namespaces
 */
using Domain.SeedWork;
using Domain.Shared;

/*
 * Framework namespaces
 */

namespace Domain.IngredientPricingAggregate
{
    public class IngredientPricingUpdated : DomainEvent
    {
        public Ingredient Ingredient { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }
        internal IngredientPricingUpdated(Ingredient ingredient, decimal oldPrice, decimal newPrice)
        {
            Ingredient = ingredient;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }
}
