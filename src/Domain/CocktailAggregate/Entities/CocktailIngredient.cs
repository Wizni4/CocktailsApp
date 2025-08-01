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
    public class CocktailIngredient : Entity
    {
        public Ingredient Ingredient { get; }
        public decimal Quantity { get => _quantity; }
        private decimal _quantity;

#pragma warning disable CS8618
        private CocktailIngredient() { } // <----- EF forced me
#pragma warning restore CS8618
        internal CocktailIngredient(Ingredient? ingredient, decimal quantity)
        {
            if (ingredient is null)
                throw new ArgumentException("Ingredient connot be null.");

            Ingredient = ingredient;
            UpdateQuantity(quantity);
        }

        internal void UpdateQuantity(decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be strictly positive.");

            _quantity = quantity;
            Touch();
        }
    }
}
