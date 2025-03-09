/*
 * Framework namespaces
 */

namespace Domain.SeedWork
{
    /// <summary>
    /// <see langword="abstract"/> class that handle <see cref="DomainEvent"/>
    /// </summary>
    /// <remarks>
    /// The <see cref="Handle(TDomainEvent)"/> method <see cref="DomainEvent.Flatten"/> the <see cref="DomainEvent"/>
    /// </remarks>
    /// <typeparam name="TDomainEvent"><see cref="DomainEvent"/> model type</typeparam>
    public abstract class DomainEventHandler<TDomainEvent> : IHandler<TDomainEvent> where TDomainEvent : DomainEvent
    {
        /// <summary>
        /// <see langword="virtual"/> method that handle <see cref="DomainEvent"/> by doing a <see cref="DomainEvent.Flatten"/>
        /// </summary>
        /// <param name="event"><see cref="DomainEvent"/> to handle</param>
        public virtual void Handle(TDomainEvent @event)
        {
            ArgumentNullException.ThrowIfNull(@event);

            @event.Flatten();
        }
    }
}
