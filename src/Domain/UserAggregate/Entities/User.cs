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
        public Guid Id { get => _id; }
        private readonly Guid _id;
        public string Username { get { return _username; } }
        private string _username;
        public string Email { get { return _email; } }
        private string _email;
        public string? Password { get { return _password; } }
        private string? _password;
        internal User(Guid id, string? username, string? email, string? password) : this(id, username, email)
        {
            _password = password;
        }
        internal User(Guid id, string? username, string? email) : base(id)
        {
            _id = id;
            _email = email ?? throw new ArgumentNullException(nameof(email));
            _username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
