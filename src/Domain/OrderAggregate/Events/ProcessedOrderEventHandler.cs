/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.StockAggregate;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.OrderAggregate
{
    public class ProcessedOrderEventHandler(IStockService stockService) : DomainEventHandler<ProcessedOrderEvent>
    {
        private readonly IStockService _stockService = stockService;

        public new void Handle(ProcessedOrderEvent @event)
        {
            _stockService.DeductStockForOrder(@event);
        }
    }
}
