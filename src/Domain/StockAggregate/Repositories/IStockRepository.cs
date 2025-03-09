/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.StockAggregate
{
    public interface IStockRepository : IRepository<Stock>
    {
    }
}
