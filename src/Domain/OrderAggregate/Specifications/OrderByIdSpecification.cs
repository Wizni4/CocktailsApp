/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.OrderAggregate
{
    public class OrderByIdSpecification(Guid orderId) : ByIdSpecification<Order>(orderId)
    {
    }
}
