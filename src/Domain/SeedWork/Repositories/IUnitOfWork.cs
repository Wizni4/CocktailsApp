/*
 * Framework namespaces
 */

namespace Domain.SeedWork
{
    public interface IUnitOfWork
    {
        IRepository<T> Set<T>() where T : Entity, IAggregateRoot;
        void SaveChanges();
    }
}
