/*
 * Framework namespaces
 */

using MediatR;

namespace CocktailsApp.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        Guid AggregateId { get; }
        Type AggregateType { get; }
        Guid ActorId { get; }
        DateTime Created { get; }
    }
}
