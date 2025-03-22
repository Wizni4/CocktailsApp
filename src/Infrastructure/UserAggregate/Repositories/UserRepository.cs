/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;
/*
 * Infrastrucute namespaces
 */
using CocktailsApp.Infrastructure.SeedWork;
/*
* Framework namespaces
*/


namespace CocktailsApp.Infrastructure.UserAggregate
{
    public class UserRepository : Repository<User>
    {
    
        public void CreateRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> ReadAllAsync(Func<IIncludable<User>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<User> ReadAsync(ISpecification<User> spec, Func<IIncludable<User>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> ReadRangeAsync(ISpecification<User> spec, Func<IIncludable<User>, IIncludable>? includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
