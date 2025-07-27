/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.OrderAggregate;

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
