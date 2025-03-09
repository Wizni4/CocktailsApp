/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.CocktailAggregate
{
    public interface ICocktailService : IService
    {
        Task<decimal> CalculateTotalPriceAsync(Cocktail cocktail);
        Task<decimal> CalculateTotalCostAsync(Cocktail cocktail);
    }
}
