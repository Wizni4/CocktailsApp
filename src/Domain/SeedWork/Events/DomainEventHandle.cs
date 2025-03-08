/*
 * Framework namespaces
 */

namespace Domain.SeedWork
{
    public class DomainEventHandle<TDomainEvent> : IHandles<TDomainEvent> where TDomainEvent : DomainEvent
    {

        public void Handle(TDomainEvent @event)
        {
            ArgumentNullException.ThrowIfNull(@event);

            @event.Flatten();
        }
    }
}
