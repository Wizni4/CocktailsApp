/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.OrderAggregate
{
    public sealed class Order : AggregateRoot, IAggregateRoot
    {
        public Guid CustomerId { get => _customerId; }
        private readonly Guid _customerId;
        public Guid ClubId { get => _clubId; }
        private readonly Guid _clubId;
        public IReadOnlyCollection<OrderItem> Items { get { return _items.AsReadOnly(); } }
        private readonly List<OrderItem> _items = [];
        public DateTime OrderDate { get => _orderDate; }
        private readonly DateTime _orderDate;
        private Order() { }
        internal Order(Guid customerId, Guid clubId) : base(customerId)
        {
            _orderDate = DateTime.UtcNow;
            _customerId = customerId;
            _clubId = clubId;

            // raise order created event
            AddDomainEvent(new OrderCreatedEvent(Id, OrderDate, CustomerId, CustomerId));
        }

        public void AddItem(Guid cocktailId, decimal quantity, Guid actorId)
        {
            _items.Add(new OrderItem(cocktailId, quantity));

            // State that entity changed
            Touch(actorId);

            // raise order item added event
            AddDomainEvent(new OrderItemAddedEvent(Id, cocktailId, quantity, actorId));
        }

        public void Process(Guid actorId)
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Cannot process an empty order.");

            AddDomainEvent(new OrderProcessedEvent(Id, actorId));
        }
    }
}
