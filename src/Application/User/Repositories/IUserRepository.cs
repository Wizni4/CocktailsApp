/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.User
{
    public interface IUserRepository : IRepository<DomainUser>
    {
    }
}
