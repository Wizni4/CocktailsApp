/*
 * Domain namespaces
 */
using Domain.SeedWork;
using Domain.IngredientPricingAggregate;

/*
 * Framework namespaces
 */

namespace Domain.CocktailAggregate
{
    public class CocktailService(IUnitOfWork unitOfWork) : Service, ICocktailService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<decimal> CalculateTotalPriceAsync(Cocktail cocktail)
        {
            var ingredientPricings = (await _unitOfWork.Set<IngredientPricing>()
                .ReadRangeAsync(new IngredientPricingSpecification([.. cocktail.Ingredients.Select(ci => ci.Ingredient)])));

            var totalPrice = cocktail.Ingredients
                .Sum(ci =>
                {
                    // Get the pricing info for the ingredient
                    var ingredientPricing = ingredientPricings.First(i => i.Ingredient == ci.Ingredient);

                    // Multiply the Price by the quantity used byt the cocktail
                    return ingredientPricing.Price * ci.Quantity;
                });

            return totalPrice;
        }

        public async Task<decimal> CalculateTotalCostAsync(Cocktail cocktail)
        {
            var ingredientPricings = await _unitOfWork.Set<IngredientPricing>()
                .ReadRangeAsync(new IngredientPricingSpecification([.. cocktail.Ingredients.Select(ci => ci.Ingredient)]));

            var totalPrice = cocktail.Ingredients
                .Sum(ci =>
                {
                    // Get the pricing info for the ingredient
                    var ingredientPricing = ingredientPricings.First(i => i.Ingredient == ci.Ingredient);

                    // Multiply the Cost by the quantity used byt the cocktail
                    return ingredientPricing.Cost * ci.Quantity;
                });

            return totalPrice;
        }
    }
}
