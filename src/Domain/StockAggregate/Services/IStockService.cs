/*
 * Domain namespaces
 */
using CocktailsApp.Domain.OrderAggregate;
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.StockAggregate
{
    public interface IStockService : IService
    {
        Task DeductStockForOrder(ProcessedOrderEvent orderEvent);
    }
}
