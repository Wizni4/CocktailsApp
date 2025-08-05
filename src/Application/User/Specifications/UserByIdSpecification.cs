/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.User
{
    public class UserByIdSpecification(Guid id) : ByIdSpecification<DomainUser>(id)
    {
    }
}
