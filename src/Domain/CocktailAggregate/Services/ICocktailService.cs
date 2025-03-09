/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public interface ICocktailService : IService
    {
        Task<decimal> CalculateTotalPriceAsync(Cocktail cocktail);
        Task<decimal> CalculateTotalCostAsync(Cocktail cocktail);
    }
}
