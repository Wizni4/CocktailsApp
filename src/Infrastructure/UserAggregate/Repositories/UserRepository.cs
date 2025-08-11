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
    public class UserRepository(EFDbContext dbContext) : EFCommandRepository<User>(dbContext), IUserRepository
    {
    }
}
