/*
 * Domain namespaces
 */
using CocktailsApp.Domain.OrderAggregate;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Stock
{
    public interface IStockService : IService<StockDTO>
    {
        Task DeductStockForOrder(ProcessedOrderEvent orderEvent);
    }
}
