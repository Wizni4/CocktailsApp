/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        private readonly List<OrderItem> _items = [];
        public IReadOnlyCollection<OrderItem> Items { get { return _items.AsReadOnly(); } }
        public DateTime OrderDate { get; }

        internal Order()
        {
            OrderDate = DateTime.UtcNow;
        }

        public void AddItem(Guid cocktailId, decimal quantity)
        {
            _items.Add(new OrderItem(cocktailId, quantity));
        }

        public void Process()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Cannot process an empty order.");

            DomainEvents.Raise(new ProcessedOrderEvent(Id));
        }
    }
}
