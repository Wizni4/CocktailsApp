/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.OrderAggregate
{
    public sealed record OrderProcessedEvent(
        Guid OrderId,
        Guid ActorId
    ) : DomainEvent(OrderId, typeof(Order), ActorId);
}
