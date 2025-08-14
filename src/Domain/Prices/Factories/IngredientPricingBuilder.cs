/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Prices
{
    public class IngredientPricingBuilder : IFactory<Pricing>
    {
        private decimal _cost;
        private Guid _creatorId;
        private Guid _ingredientId;
        private decimal _price;

        public IngredientPricingBuilder WithIngredient(Guid ingredientId)
        {
            _ingredientId = ingredientId;
            return this;
        }

        public IngredientPricingBuilder WithCost(decimal cost)
        {
            _cost = cost;
            return this;
        }

        public IngredientPricingBuilder WithCreatorId(Guid creatorId)
        {
            _creatorId = creatorId;
            return this;
        }

        public IngredientPricingBuilder WithPrice(decimal price)
        {
            _price = price;
            return this;
        }

        public Pricing Build()
        {
            return new Pricing(_ingredientId, _cost, _price, _creatorId);
        }
    }
}
