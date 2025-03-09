/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class CocktailIngredientBuilder : IBuilder<CocktailIngredient>
    {
        private Ingredient? _ingredient;
        private decimal _quantity = 0;

        public CocktailIngredientBuilder AddIngredient(Ingredient ingredient)
        {
            _ingredient = ingredient;
            return this;
        }

        public CocktailIngredientBuilder AddQuantity(decimal quantity)
        {
            _quantity = quantity;
            return this;
        }

        public CocktailIngredient Build()
        {
            return new CocktailIngredient(_ingredient, _quantity);
        }
    }
}
