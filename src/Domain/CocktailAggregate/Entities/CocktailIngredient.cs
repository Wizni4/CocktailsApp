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
    public class CocktailIngredient : ValueObject
    {
        public Ingredient Ingredient { get; }
        public decimal Quantity { get; }

        internal CocktailIngredient(Ingredient? ingredient, decimal quantity)
        {
            ArgumentNullException.ThrowIfNull(ingredient);
            Ingredient = ingredient;
            Quantity = quantity;
        }
    }
}
