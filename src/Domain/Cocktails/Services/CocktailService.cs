/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;
using CocktailsApp.Domain.Prices;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Cocktails
{
    public class CocktailService() : Service, ICocktailService
    {
        public decimal CalculateTotalPriceAsync(Cocktail cocktail, List<Pricing> ingredientPricings)
        {
            var totalPrice = cocktail.Ingredients
                .Sum(ci =>
                {
                    // Get the pricing info for the ingredient
                    var ingredientPricing = GetIngredientPricing(ingredientPricings, ci);

                    // Multiply the Price by the quantity used byt the cocktail
                    return ingredientPricing.Price * ci.Quantity;
                });

            return totalPrice;
        }

        public decimal CalculateTotalCostAsync(Cocktail cocktail, List<Pricing> ingredientPricings)
        {
            var totalPrice = cocktail.Ingredients
                .Sum(ci =>
                {
                    // Get the pricing info for the ingredient
                    var ingredientPricing = GetIngredientPricing(ingredientPricings, ci);

                    // Multiply the Cost by the quantity used byt the cocktail
                    return ingredientPricing.Cost * ci.Quantity;
                });

            return totalPrice;
        }

        private Pricing GetIngredientPricing(List<Pricing> ingredientPricings, CocktailIngredient cocktailIngredient)
        {
            // Get the pricing info for the ingredient
            var ingredientPricing = ingredientPricings.FirstOrDefault(i => new CocktailIngredientByIngredientSpecification(i.IngredientId).IsSatisfiedBy(cocktailIngredient))
                        ?? throw new ArgumentException($"There is no pricing available for ingredient '{cocktailIngredient.IngredientId}'.");

            return ingredientPricing;
        }
    }
}
