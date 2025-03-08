/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.OrderAggregate
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
