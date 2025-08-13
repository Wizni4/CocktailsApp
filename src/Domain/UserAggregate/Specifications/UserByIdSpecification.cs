/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.UserAggregate
{
    public class UserByIdSpecification(Guid id) : ByIdSpecification<User>(id)
    {
    }
}
