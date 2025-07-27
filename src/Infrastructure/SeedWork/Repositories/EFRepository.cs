/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
* Framework namespaces
*/
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.Infrastructure.SeedWork
{
    /// <summary>
    /// Represents a generic repository for aggregate roots that encapsulates common data access operations.
    /// Implements the <see cref="IRepository{T}"/> interface and provides CRUD operations following DDD principles.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the aggregate root entity. Must inherit from <see cref="Entity"/> and implement <see cref="IAggregateRoot"/>.
    /// </typeparam>
    public class EFRepository<T>(EFDbContext dbContext) : IRepository<T> where T : Entity, IAggregateRoot
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a single entity from the data store.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a collection of entities from the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously retrieves all entities of type <typeparamref name="T"/>, optionally including related data.
        /// </summary>
        /// <param name="includes">A function to define related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of all entities.</returns>
        public Task<IEnumerable<T>> ReadAllAsync(Func<IIncludable<T>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously retrieves a single entity that matches the given specification, optionally including related data.
        /// </summary>
        /// <param name="spec">The specification that defines the query criteria.</param>
        /// <param name="includes">A function to define related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the matching entity.</returns>
        public Task<T> ReadAsync(ISpecification<T> spec, Func<IIncludable<T>, IIncludable>? includes = null)
        {
            if (spec == null)
                throw new ArgumentNullException(nameof(spec));

            return Task.FromResult(_dbContext.Set<T>().IncludeMultiples(includes).FirstOrDefault(spec.SpecExpression));
        }

        /// <summary>
        /// Asynchronously retrieves a collection of entities that match the given specification, optionally including related data.
        /// </summary>
        /// <param name="spec">The specification that defines the query criteria.</param>
        /// <param name="includes">A function to define related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of matching entities.</returns>
        public Task<IEnumerable<T>> ReadRangeAsync(ISpecification<T> spec, Func<IIncludable<T>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing entity in the data store.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a collection of entities in the data store.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        public void UpdateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
