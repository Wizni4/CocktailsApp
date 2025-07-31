/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;

using DomainIngredientPricing =  CocktailsApp.Domain.IngredientPricingAggregate.IngredientPricing;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.IngredientPricing
{
    public interface IIngredientPricingRepository : IRepository<DomainIngredientPricing>
    {
    }
}
