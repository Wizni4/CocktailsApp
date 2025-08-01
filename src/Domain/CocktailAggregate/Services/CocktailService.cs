/*
 * Domain namespaces
 */
using CocktailsApp.Domain.IngredientPricingAggregate;
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class CocktailService() : Service, ICocktailService
    {
        public decimal CalculateTotalPriceAsync(Cocktail cocktail, List<IngredientPricing> ingredientPricings)
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

        public decimal CalculateTotalCostAsync(Cocktail cocktail, List<IngredientPricing> ingredientPricings)
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

        private IngredientPricing GetIngredientPricing(List<IngredientPricing> ingredientPricings, CocktailIngredient cocktailIngredient)
        {
            // Get the pricing info for the ingredient
            var ingredientPricing = ingredientPricings.FirstOrDefault(i => new CocktailIngredientByIngredientSpecification(i.Ingredient).SpecExpression.Compile()(cocktailIngredient))
                        ?? throw new ArgumentException($"There is no pricing available for ingredient '{cocktailIngredient.Ingredient.Name}'.");

            return ingredientPricing;
        }
    }
}
