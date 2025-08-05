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
    public class UserByIdsSpecification(List<Guid> ids) : ByIdsSpecification<DomainUser>(ids)
    {
    }
}
