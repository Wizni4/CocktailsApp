/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.UserAggregate
{
    public sealed class User : AggregateRoot, IAggregateRoot
    {
        public new Guid Id { get; }
        private string _username;
        public string Username { get { return _username; } }
        private string _email;
        public string Email { get { return _email; } }
        private string? _password;
        public string? Password { get { return _password; } }
        internal User(Guid id, string? username, string? email, string? password): this(id,username, email)
        {
            _password = password;
        }
        internal User(Guid id, string? username, string? email) : this(username, email)
        {
            Id = id;
            _email = email ?? throw new ArgumentNullException(nameof(email));
            _username = username ?? throw new ArgumentNullException(nameof(username));
        }
        internal User(string? username, string? email)
        {
            _email = email ?? throw new ArgumentNullException(nameof(email));
            _username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
