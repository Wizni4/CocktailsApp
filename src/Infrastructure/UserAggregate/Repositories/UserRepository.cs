/*
 * Domain namespaces
 */
using CocktailsApp.Application.User;
using CocktailsApp.Domain.UserAggregate;
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
