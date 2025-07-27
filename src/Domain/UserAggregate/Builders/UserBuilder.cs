/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.UserAggregate
{
    public class UserBuilder : IBuilder<User>
    {
        public User Build()
        {
            return new User();
        }
    }
}
