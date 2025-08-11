/*
 * Framework namespaces
 */

using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.SeedWork
{
    /// <summary>
    /// Generic repository that describe CRUD methods.
    /// </summary>
    /// <typeparam name="T">Domain model type</typeparam>
    public interface IRepository<T> where T : Entity, IAggregateRoot
    {
        #region Create methods
        /// <summary>
        /// Create a new domain model.
        /// </summary>
        /// <param name="entity">Domain model to create</param>
        void Create(T entity);
        /// <summary>
        /// Create a list of new domain models.
        /// </summary>
        /// <param name="entities">List of domain models to create</param>
        void CreateRange(IEnumerable<T> entities);
        #endregion

        #region Read methods
        /// <summary>
        /// Get a domain model, according to a condition.
        /// </summary>
        /// <param name="spec">Condition</param>
        /// <param name="includes">Properties that should be eagerly loaded</param>
        /// <returns>Domain model corresponding to the condition, including specified sub-properties</returns>
        Task<T?> ReadAsync(ICommandSpecification<T> spec);
        /// <summary>
        /// Get a list of domain models, according to a condition.
        /// </summary>
        /// <param name="spec">Condition</param>
        /// <param name="includes">Properties that should be eagerly loaded</param>
        /// <returns>List of domain models corresponding to the condition, including specified sub-properties</returns>
        Task<IEnumerable<T>> ReadRangeAsync(ICommandSpecification<T> spec);
        /// <summary>
        /// Get all the domain models.
        /// </summary>
        /// <param name="includes">Properties that should be eagerly loaded</param>
        /// <returns>All the domain models, including specified sub-properties</returns>
        Task<IEnumerable<T>> ReadAllAsync(ICommandSpecification<T> spec);
        #endregion

        #region Update methods
        /// <summary>
        /// Update a domain model.
        /// </summary>
        /// <param name="entity">Domain model to update</param>
        /// <returns>Domain model updated</returns>
        void Update(T entity);
        /// <summary>
        /// Update a list of domain models.
        /// </summary>
        /// <param name="entities">List of domain models to update</param>
        /// <returns>List of domain models updated</returns>
        void UpdateRange(IEnumerable<T> entities);
        #endregion

        #region Delete methods
        /// <summary>
        /// Delete a domain model.
        /// </summary>
        /// <param name="entity">Domain model to delete</param>
        void Delete(T entity);
        /// <summary>
        /// Delete a list of domain models.
        /// </summary>
        /// <param name="entities">List of domain models to delete</param>
        void DeleteRange(IEnumerable<T> entities);
        #endregion
    }
}
