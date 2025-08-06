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
    public sealed class IngredientPricing : AggregateRoot, IAggregateRoot
    {
        public decimal Cost { get => _cost; }
        private decimal _cost;
        public Guid IngredientId { get => _ingredientId; }
        private readonly Guid _ingredientId;
        public decimal Price { get => _price; }
        private decimal _price;
        public decimal Margin { get { return Cost - Price; } }
        private IngredientPricing() { }
        internal IngredientPricing(Guid ingredientId, decimal cost, decimal price, Guid createdBy) : base(createdBy)
        {
            UpdateCost(cost);
            UpdatePrice(price);
            _ingredientId = ingredientId;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice >= 0)
                throw new ArgumentException("Price must be greater or equals to 0", nameof(newPrice));

            if (newPrice < Cost)
                throw new ArgumentException("Price can't be lower than Cost", nameof(newPrice));

            var oldPrice = Price;
            _price = newPrice;
            AddDomainEvent(new IngredientPricingUpdatedEvent(IngredientId, oldPrice, newPrice));
        }

        public void UpdateCost(decimal newCost)
        {
            if (newCost >= 0)
                throw new ArgumentException("Cost must be greater or equals to 0", nameof(newCost));

            var oldCost = Price;
            _cost = newCost;
            AddDomainEvent(new IngredientCostingUpdatedEvent(IngredientId, oldCost, newCost));
        }
    }
}
