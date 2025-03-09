/*
 * Framework namespaces
 */

namespace Domain.SeedWork
{
    /// <summary>
    /// Interface that represents a unit of work in the domain, which provides a way to group and manage repositories.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Retrieves the repository for the <see cref="IAggregateRoot"/> entity type.
        /// </summary>
        /// <typeparam name="T">The type of entity, which must be an <see cref="Entity"/> and implement <see cref="IAggregateRoot"/>.</typeparam>
        /// <returns>An instance of <see cref="IRepository{T}"/></returns>
        IRepository<T> Set<T>() where T : Entity, IAggregateRoot;
        /// <summary>
        /// Asynchronously saves all changes made within the unit of work to the data store.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        Task SaveChangesAsync();
    }
}
