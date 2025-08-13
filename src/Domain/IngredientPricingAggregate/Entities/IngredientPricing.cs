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
            SetCost(cost);
            SetPrice(price);
            _ingredientId = ingredientId;

            // raise ingredient pricing created event
            AddDomainEvent(new IngredientPricingCreatedEvent(Id, Cost, Price, createdBy));
        }

        public void DeleteIngredientPricing(Guid actorId)
        {
            // State that entity changed
            Touch(actorId);

            // raise ingredient pricing deleted
            AddDomainEvent(new IngredientPricingDeletedEvent(Id, actorId));
        }

        public void UpdateCost(decimal newCost, Guid actorId)
        {
            var oldCost = Cost;
            SetCost(newCost);

            // State that entity changed
            Touch(actorId);

            // Raise cost changed event
            AddDomainEvent(new IngredientCostChangedEvent(IngredientId, oldCost, newCost, actorId));
        }

        public void UpdatePrice(decimal newPrice, Guid actorId)
        {
            var oldPrice = Price;
            SetPrice(newPrice);

            // State that entity changed
            Touch(actorId);

            // Raise price changed event
            AddDomainEvent(new IngredientPriceChangedEvent(IngredientId, oldPrice, newPrice, actorId));
        }

        private void SetCost(decimal cost)
        {
            if (cost >= 0)
                throw new ArgumentException("Cost must be greater or equals to 0", nameof(cost));

            _cost = cost;
        }

        private void SetPrice(decimal price)
        {
            if (price >= 0)
                throw new ArgumentException("Price must be greater or equals to 0", nameof(price));

            if (price < Cost)
                throw new ArgumentException("Price can't be lower than Cost", nameof(price));

            _price = price;
        }
    }
}
