/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> AuthenticateUserAsync(User entity);
    }
}
