/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.OrderAggregate
{
    public class OrderByIdSpecification(Guid orderId) : ByIdSpecification<Order>(orderId)
    {
    }
}
