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
    public class IngredientPricingBuilder : IBuilder<IngredientPricing>
    {
        private decimal _cost;
        private Ingredient? _ingredient;
        private decimal _price;

        public IngredientPricingBuilder WithIngredient(Ingredient ingredient)
        {
            _ingredient = ingredient;
            return this;
        }

        public IngredientPricingBuilder WithCost(decimal cost)
        {
            _cost = cost;
            return this;
        }

        public IngredientPricingBuilder WithPrice(decimal price)
        {
            _price = price;
            return this;
        }

        public IngredientPricing Build()
        {
            return new IngredientPricing(_ingredient, _cost, _price);
        }
    }
}
