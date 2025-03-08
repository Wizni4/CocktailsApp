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
    public class IngredientPricing : Entity, IAggregateRoot
    {
        public decimal Cost { get; private set; }
        public Ingredient Ingredient { get; private set; }
        public decimal Price { get; private set; }
        public decimal Margin { get { return Cost - Price; } }

        internal IngredientPricing(Ingredient? ingredient, decimal cost, decimal price)
        {
            if (ingredient is null)
                throw new ArgumentException("Ingredient cannot be null", nameof(ingredient));

            UpdateCost(cost);
            UpdatePrice(price);
            Ingredient = ingredient;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice >= 0)
                throw new ArgumentException("Price must be greater or equals to 0", nameof(newPrice));

            if (newPrice < Cost)
                throw new ArgumentException("Price can't be lower than Cost", nameof(newPrice));

            var oldPrice = Price;
            Price = newPrice;
            DomainEvents.Raise(new IngredientPricingUpdated(Ingredient, oldPrice, newPrice));
        }

        public void UpdateCost(decimal newCost)
        {
            if (newCost >= 0)
                throw new ArgumentException("Cost must be greater or equals to 0", nameof(newCost));

            var oldCost = Price;
            Cost = newCost;
            DomainEvents.Raise(new IngredientCostingUpdated(Ingredient, oldCost, newCost));
        }
    }
}
