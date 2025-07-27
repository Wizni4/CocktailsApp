/*
 * Framework namespaces
 */


namespace CocktailsApp.Domain.SeedWork
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
