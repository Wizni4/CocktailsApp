/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;

using DomainStock = CocktailsApp.Domain.StockAggregate.Stock;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Stock
{
    public interface IStockRepository : IRepository<DomainStock>
    {
    }
}
