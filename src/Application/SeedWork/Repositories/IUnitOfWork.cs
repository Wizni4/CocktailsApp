/*
 * Framework namespaces
 */

using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.SeedWork
{
    /// <summary>
    /// Interface that represents a unit of work in the domain, which provides a way to group and manage repositories.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Asynchronously saves all changes made within the unit of work to the data store.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        Task SaveChangesAsync();
    }
}
