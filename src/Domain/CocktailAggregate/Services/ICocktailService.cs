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
    public interface ICocktailService : IService
    {
        decimal CalculateTotalPriceAsync(Cocktail cocktail, List<IngredientPricing> ingredientPricings);
        decimal CalculateTotalCostAsync(Cocktail cocktail, List<IngredientPricing> ingredientPricings);
    }
}
