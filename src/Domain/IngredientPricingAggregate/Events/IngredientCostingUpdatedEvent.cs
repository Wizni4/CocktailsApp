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
        public Ingredient Ingredient { get; }
        public decimal OldCost { get; }
        public decimal NewCost { get; }
        internal IngredientCostingUpdatedEvent(Ingredient ingredient, decimal oldCost, decimal newCost)
        {
            Ingredient = ingredient;
            OldCost = oldCost;
            NewCost = newCost;
        }
    }
}
