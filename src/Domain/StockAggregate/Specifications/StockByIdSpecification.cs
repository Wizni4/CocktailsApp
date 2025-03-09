/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.StockAggregate
{
    public class StockByIdSpecification(Guid id) : ByIdSpecification<Stock>(id)
    {
    }
}
