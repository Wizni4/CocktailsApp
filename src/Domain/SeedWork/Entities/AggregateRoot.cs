/*
 * Framework namespaces
 */


namespace CocktailsApp.Domain.SeedWork
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private protected AggregateRoot() { }
        private protected AggregateRoot(Guid createdBy) : base(createdBy) { }
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        private readonly List<IDomainEvent> _domainEvents = new();
        public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
