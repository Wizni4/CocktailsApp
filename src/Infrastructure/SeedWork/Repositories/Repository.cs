/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
* Framework namespaces
*/


namespace CocktailsApp.Infrastructure.SeedWork
{
    public class Repository<T> : IRepository<T> where T: Entity, IAggregateRoot
    {
        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ReadAllAsync(Func<IIncludable<T>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadAsync(ISpecification<T> spec, Func<IIncludable<T>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ReadRangeAsync(ISpecification<T> spec, Func<IIncludable<T>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
