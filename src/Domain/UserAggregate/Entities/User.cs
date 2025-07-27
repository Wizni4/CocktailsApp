/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.UserAggregate
{
    public class User : AggregateRoot, IAggregateRoot
    {
        private string _login;
        public string Login { get { return _login; } }
        private string _password;
        public string Password { get { return _password; } }
        internal User(string? login, string? password)
        {
            _login = login ?? throw new ArgumentNullException(nameof(login));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
