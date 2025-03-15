/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubRole : Entity
    {
        public string Name { get; private set; }
        private readonly List<ClubPermission> _permissions = [];
        public IReadOnlyCollection<ClubPermission> Permissions { get { return _permissions.AsReadOnly(); } }
        internal ClubRole(string name)
        {
            Name = name;
        }

        internal void AddPermission(ClubPermission permission)
        {
            // Ensure the role doesn't already have this permission.
            if (_permissions.Any(p => p == permission))
                throw new ArgumentException("This role already have specified permission", nameof(permission));

            // Give the permission to the role.
            // FYI: As Permission is a ValueObject, instanciating a new Permission will not create new entry in the database.
            _permissions.Add(permission);
        }

        internal void RemovePermission(ClubPermission permission)
        {
            _permissions.Remove(permission);
        }

        internal void UpdateName(string newName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(newName, nameof(newName));
            Name = newName;

        }

        internal bool HasPermission(ClubPermission action)
        {
            return Permissions.Any(p => p == action);
        }
    }
}
