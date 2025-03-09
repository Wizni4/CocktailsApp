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
        public Ingredient Ingredient { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }
        internal IngredientPricingUpdatedEvent(Ingredient ingredient, decimal oldPrice, decimal newPrice)
        {
            Ingredient = ingredient;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }
}
