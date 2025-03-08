/*
 * Domain namespaces
 */
using Domain.OrderAggregate;
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.StockAggregate
{
    public interface IStockService : IService
    {
        Task DeductStockForOrder(ProcessedOrderEvent orderEvent);
    }
}
