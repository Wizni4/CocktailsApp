/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; }
        public string Password { get; }
        public string Email { get; }

        internal User(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
        }
    }
}
