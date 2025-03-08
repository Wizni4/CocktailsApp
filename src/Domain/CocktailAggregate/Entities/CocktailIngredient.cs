/*
 * Domain namespaces
 */
using Domain.SeedWork;
using Domain.Shared;

/*
 * Framework namespaces
 */

namespace Domain.CocktailAggregate
{
    public class CocktailIngredient : ValueObject
    {
        public Ingredient Ingredient { get; private set; }
        public decimal Quantity { get; private set; }

        internal CocktailIngredient(Ingredient? ingredient, decimal quantity)
        {
            ArgumentNullException.ThrowIfNull(ingredient);
            Ingredient = ingredient;
            Quantity = quantity;
        }

        private protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Ingredient;
            yield return Quantity;
        }
    }
}
