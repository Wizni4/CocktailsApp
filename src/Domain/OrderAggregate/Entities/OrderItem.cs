/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public Guid CocktailId { get; }
        public decimal Quantity { get; }

        internal OrderItem(Guid cocktailId, decimal quantity)
        {
            CocktailId = cocktailId;
            Quantity = quantity;
        }
    }
}
