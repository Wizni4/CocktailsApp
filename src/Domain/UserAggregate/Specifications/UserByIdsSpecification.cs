/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.UserAggregate
{
    public class UserByIdsSpecification(List<Guid> ids) : ByIdsSpecification<User>(ids)
    {
    }
}
