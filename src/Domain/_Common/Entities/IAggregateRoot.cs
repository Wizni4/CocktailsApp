/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Common
{
    /// <summary>
    /// Marker interface that identifies an entity as an aggregate root in the domain model.
    /// </summary>
    public interface IAggregateRoot
    {
        public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void AddDomainEvent(IDomainEvent domainEvent);
        void ClearDomainEvents();
    }
}
