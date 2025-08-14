/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Users
{
    public class UserFactory : IFactory<User>
    {
        private Guid _id = Guid.NewGuid();
        private string? _email;
        private string? _username;
        private string? _password;
        public UserFactory WithId(Guid id)
        {
            _id = id;
            return this;
        }
        public UserFactory WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public UserFactory WithUsername(string username)
        {
            _username = username;
            return this;
        }
        public UserFactory WithPassword(string password)
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
