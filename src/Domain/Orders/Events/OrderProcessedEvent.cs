/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Orders
{
    public sealed record OrderProcessedEvent(
        Guid OrderId,
        Guid ActorId
    ) : DomainEvent(OrderId, typeof(Order), ActorId);
}
