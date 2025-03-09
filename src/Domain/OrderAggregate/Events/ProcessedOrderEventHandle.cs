/*
 * Domain namespaces
 */
using Domain.SeedWork;
using Domain.StockAggregate;

/*
 * Framework namespaces
 */

namespace Domain.OrderAggregate
{
    public class ProcessedOrderEventHandle(IStockService stockService) : DomainEventHandler<ProcessedOrderEvent>
    {
        private readonly IStockService _stockService = stockService;

        public new void Handle(ProcessedOrderEvent @event)
        {
            _stockService.DeductStockForOrder(@event);
        }
    }
}
