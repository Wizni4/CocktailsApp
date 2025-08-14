/*
 * Domain namespaces
 */
using CocktailsApp.Application.Users;
using CocktailsApp.Domain.Users;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Persistence;

/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.Users
{
    public class UserRepository(
        EFWriteDbContext dbContext,
        IIncludesService<User> includesService
    ) : EFRepository<User>(dbContext, includesService), IUserRepository
    {
    }
}
