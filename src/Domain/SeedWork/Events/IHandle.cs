/*
 * Framework namespaces
 */

namespace Domain.SeedWork
{
    public interface IHandles<T> where T : DomainEvent
    {
        void Handle(T args);
    }
}
