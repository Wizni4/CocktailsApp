/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using System.Runtime.CompilerServices;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.UserAggregate
{
    public class UserBuilder : IBuilder<User>
    {
        private string? _login;
        private string? _password;

        public UserBuilder WithLogin(string login)
        {
            _login = login;
            return this;
        }
        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }
        public User Build()
        {
            return new User(_login, _password);
        }
    }
}
