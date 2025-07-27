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

#pragma warning disable CS8618
        private CocktailIngredient() { } // <----- EF forced me
#pragma warning restore CS8618
        internal CocktailIngredient(Ingredient? ingredient, decimal quantity)
        {
            ArgumentNullException.ThrowIfNull(ingredient);
            Ingredient = ingredient;
            Quantity = quantity;
        }
    }
}
