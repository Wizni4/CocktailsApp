/*
*Domain namespaces
*/
using CocktailsApp.Domain.OrderAggregate;
using CocktailsApp.Domain.SeedWork;
/*
 * Application namespaces
 */
using CocktailsApp.Application.Stock;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Order
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
