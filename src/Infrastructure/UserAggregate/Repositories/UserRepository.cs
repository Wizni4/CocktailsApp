/*
 * Domain namespaces
 */
using CocktailsApp.Application.Users;
using CocktailsApp.Domain.Users;
using CocktailsApp.Infrastructure.SeedWork;

/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.UserAggregate
{
    public class UserRepository(EFWriteDbContext dbContext)
        : EFRepository<User>(dbContext), IUserRepository
    {
    }
}
