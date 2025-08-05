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
        public Guid CustomerId { get; }
        public Guid ClubId { get; }
        private readonly List<OrderItem> _items = [];
        public IReadOnlyCollection<OrderItem> Items { get { return _items.AsReadOnly(); } }
        public DateTime OrderDate { get; }

        internal Order(Guid customerId, Guid clubId)
        {
            OrderDate = DateTime.UtcNow;
            CustomerId = customerId;
            ClubId = clubId;
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
