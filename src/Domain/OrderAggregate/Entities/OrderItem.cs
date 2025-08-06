/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.OrderAggregate
{
    public sealed class OrderItem : ValueObject
    {
        public Guid CocktailId { get => _cocktailId; }
        private readonly Guid _cocktailId;
        public decimal Quantity { get => _quantity; }
        private readonly decimal _quantity;
        private OrderItem() { }
        internal OrderItem(Guid cocktailId, decimal quantity)
        {
            _cocktailId = cocktailId;
            _quantity = quantity;
        }
    }
}
