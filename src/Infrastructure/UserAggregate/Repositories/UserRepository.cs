/*
 * Domain namespaces
 */
using CocktailsApp.Domain.UserAggregate;
using CocktailsApp.Infrastructure.SeedWork;

/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.UserAggregate
{
    public class UserRepository(EFDbContext dbContext) : EFRepository<User>(dbContext), IUserRepository
    {
        public Task<bool> AuthenticateUserAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
