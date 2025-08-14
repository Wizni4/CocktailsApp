/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Prices;
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Cocktails
{
    public interface ICocktailService : IService
    {
        decimal CalculateTotalPriceAsync(Cocktail cocktail, List<Pricing> ingredientPricings);
        decimal CalculateTotalCostAsync(Cocktail cocktail, List<Pricing> ingredientPricings);
    }
}
