/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> AuthenticateUserAsync(User entity);
    }
}
