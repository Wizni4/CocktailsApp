/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.Common;

using Microsoft.EntityFrameworkCore;
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
    public abstract class EFRepository<T>(EFWriteDbContext dbContext) : IRepository<T> where T : Entity, IAggregateRoot
    {
        private protected readonly EFWriteDbContext DbContext = dbContext;

        /// <summary>
        /// Persists a single entity to the data store.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        public virtual Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return DbContext.Set<T>().AddAsync(entity, cancellationToken).AsTask();
        }

        /// <summary>
        /// Persists a collection of entities to the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to create.</param>
        public virtual Task CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));

            return DbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Deletes a single entity from the data store.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Deletes a collection of entities from the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        public virtual Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));

            DbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously retrieves all entities of type <typeparamref name="T"/>, optionally including related data.
        /// </summary>
        /// <param name="includes">A function to define related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of all entities.</returns>
        public virtual Task<IEnumerable<T>> ReadAllAsync(ICommandSpecification<T> spec, CancellationToken cancellationToken)
        {
            var query = DbContext.Set<T>().AsQueryable();

            // Add include to the query
            if (spec.Includes != null)
                query = query.IncludeMultiples(spec.Includes);

            return query.ToListAsync(cancellationToken)
                        .ContinueWith(a => a.Result.AsEnumerable());
        }

        /// <summary>
        /// Asynchronously retrieves a single entity that matches the given specification, optionally including related data.
        /// </summary>
        /// <param name="spec">The specification that defines the query criteria.</param>
        /// <param name="includes">A function to define related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the matching entity.</returns>
        public virtual Task<T?> ReadAsync(ICommandSpecification<T> spec, CancellationToken cancellationToken)
        {
            var query = DbContext.Set<T>().AsQueryable();

            // Add include to the query
            if (spec.Includes != null)
                query = query.IncludeMultiples(spec.Includes);

            return query.FirstOrDefaultAsync(spec.Specification!.SpecExpression, cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves a collection of entities that match the given specification, optionally including related data.
        /// </summary>
        /// <param name="spec">The specification that defines the query criteria.</param>
        /// <param name="includes">A function to define related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of matching entities.</returns>
        public virtual Task<IEnumerable<T>> ReadRangeAsync(ICommandSpecification<T> spec, CancellationToken cancellationToken)
        {
            var query = DbContext.Set<T>().AsQueryable();

            // Add include to the query
            if (spec.Includes != null)
                query = query.IncludeMultiples(spec.Includes);

            // apply the filter
            query = query.Where(spec.Specification!.SpecExpression);

            return query.ToListAsync(cancellationToken)
                        .ContinueWith(a => a.Result.AsEnumerable());
        }

        /// <summary>
        /// Updates an existing entity in the data store.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbContext.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates a collection of entities in the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        public virtual Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));

            DbContext.UpdateRange(entities);
            return Task.CompletedTask;
        }
    }
}
