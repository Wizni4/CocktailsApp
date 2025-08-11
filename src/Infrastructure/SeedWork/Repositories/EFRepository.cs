/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.SeedWork
{
    /// <summary>
    /// Represents a generic repository for aggregate roots that encapsulates common data access operations.
    /// Implements the <see cref="IRepository{T}"/> interface and provides CRUD operations following DDD principles.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the aggregate root entity. Must inherit from <see cref="Entity"/> and implement <see cref="IAggregateRoot"/>.
    /// </typeparam>
    public class EFCommandRepository<T>(EFDbContext dbContext) : IRepository<T> where T : Entity, IAggregateRoot
    {
        private readonly EFDbContext _dbContext = dbContext;

        /// <summary>
        /// Persists a single entity to the data store.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        public void Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<T>().Add(entity); ;
        }

        /// <summary>
        /// Persists a collection of entities to the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to create.</param>
        public void CreateRange(IEnumerable<T> entities)
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));

            _dbContext.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Deletes a single entity from the data store.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Deletes a collection of entities from the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));

            _dbContext.Set<T>().RemoveRange(entities);
        }

        /// <summary>
        /// Asynchronously retrieves all entities of type <typeparamref name="T"/>, optionally including related data.
        /// </summary>
        /// <param name="includes">A function to define related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of all entities.</returns>
        public Task<IEnumerable<T>> ReadAllAsync(ICommandSpecification<T> spec)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            // Add include to the query
            if (spec.Includes != null)
                query = query.IncludeMultiples(spec.Includes);

            return Task.FromResult(query.AsEnumerable());
        }

        /// <summary>
        /// Asynchronously retrieves a single entity that matches the given specification, optionally including related data.
        /// </summary>
        /// <param name="spec">The specification that defines the query criteria.</param>
        /// <param name="includes">A function to define related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the matching entity.</returns>
        public Task<T?> ReadAsync(ICommandSpecification<T> spec)
        {
            if (spec == null)
                throw new ArgumentNullException(nameof(spec));

            var query = _dbContext.Set<T>().AsQueryable();

            // Add include to the query
            if (spec.Includes != null)
                query = query.IncludeMultiples(spec.Includes);

            return Task.FromResult(query.FirstOrDefault(spec.Specification!.SpecExpression));
        }

        /// <summary>
        /// Asynchronously retrieves a collection of entities that match the given specification, optionally including related data.
        /// </summary>
        /// <param name="spec">The specification that defines the query criteria.</param>
        /// <param name="includes">A function to define related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of matching entities.</returns>
        public Task<IEnumerable<T>> ReadRangeAsync(ICommandSpecification<T> spec)
        {
            if (spec == null)
                throw new ArgumentNullException(nameof(spec));

            var query = _dbContext.Set<T>().AsQueryable();

            // Add include to the query
            if (spec.Includes != null)
                query = query.IncludeMultiples(spec.Includes);

            // apply the filter
            query = query.Where(spec.Specification!.SpecExpression);


            return Task.FromResult(query.AsEnumerable());
        }

        /// <summary>
        /// Updates an existing entity in the data store.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Update(entity);
        }

        /// <summary>
        /// Updates a collection of entities in the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        public void UpdateRange(IEnumerable<T> entities)
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));

            _dbContext.UpdateRange(entities);
        }
    }
}
