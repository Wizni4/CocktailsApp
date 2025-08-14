/*
 * Framework namespaces
 */


namespace CocktailsApp.Domain.Common
{
    /// <summary>
    /// <see langword="abstract"/> class that represents a domain event in the system, which is an event that signifies a change in the domain model.<br/>
    /// </summary>
    public abstract record DomainEvent(
        Guid AggregateId,
        Type AggregateType,
        Guid ActorId
    ) : IDomainEvent
    {
        /// <summary>
        /// Gets the <see cref="DateTime"/> of when the event was created.
        /// </summary>
        public virtual DateTime Created { get; } = DateTime.Now;
    }
}
