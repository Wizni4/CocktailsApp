/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

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
        private readonly List<OrderItem> _items = [];
        public IReadOnlyCollection<OrderItem> Items { get { return _items.AsReadOnly(); } }
        public DateTime OrderDate { get => _orderDate; }
        private readonly DateTime _orderDate;
        private Order() { }
        internal Order(Guid customerId, Guid clubId) : base(customerId)
        {
            _orderDate = DateTime.UtcNow;
            _customerId = customerId;
            _clubId = clubId;
        }

        public void AddItem(Guid cocktailId, decimal quantity)
        {
            _items.Add(new OrderItem(cocktailId, quantity));
        }

        public void Process()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Cannot process an empty order.");

            AddDomainEvent(new ProcessedOrderEvent(Id));
        }
    }
}
