/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.OrderAggregate
{
    public class ProcessedOrderEvent : DomainEvent
    {
        public Guid OrderId { get; }
        internal ProcessedOrderEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
