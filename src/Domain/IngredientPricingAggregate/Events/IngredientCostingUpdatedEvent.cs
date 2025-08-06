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
    public class IngredientCostingUpdatedEvent : DomainEvent
    {
        public Guid IngredientId { get; }
        public decimal OldCost { get; }
        public decimal NewCost { get; }
        internal IngredientCostingUpdatedEvent(Guid ingredientId, decimal oldCost, decimal newCost)
        {
            IngredientId = ingredientId;
            OldCost = oldCost;
            NewCost = newCost;
        }
    }
}
