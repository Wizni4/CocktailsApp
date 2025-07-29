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
        private Guid _id = Guid.NewGuid();
        private string? _email;
        private string? _username;
        private string? _password;
        public UserBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public UserBuilder WithUsername(string username)
        {
            _username = username;
            return this;
        }
        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }
        public User Build()
        {
            return new User(_id, _username, _email, _password);
        }
    }
}
