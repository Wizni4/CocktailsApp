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
    public class IngredientCostingUpdated : DomainEvent
    {
        public Ingredient Ingredient { get; }
        public decimal OldCost { get; }
        public decimal NewCost { get; }
        internal IngredientCostingUpdated(Ingredient ingredient, decimal oldCost, decimal newCost)
        {
            Ingredient = ingredient;
            OldCost = oldCost;
            NewCost = newCost;
        }
    }
}
