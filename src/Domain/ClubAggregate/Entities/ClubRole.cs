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
        private readonly List<Permission> _permissions = [];
        public IReadOnlyCollection<Permission> Permissions { get { return _permissions.AsReadOnly(); } }
        internal ClubRole(string name)
        {
            Name = name;
        }

        internal void AddPermission(ClubAction action)
        {
            // Ensure the roled doesn't already have this permission.
            if (_permissions.Any(p => p.Action == action))
                throw new ArgumentException("This role already have specified permission", nameof(action));

            // Give the permission to the role.
            // FYI: As Permission is a ValueObject, instanciating a new Permission will not create new entry in the database.
            _permissions.Add(new Permission(action));
        }

        internal void AddPermissions(IEnumerable<ClubAction> actions)
        {
            foreach (var action in actions)
                AddPermission(action);
        }

        internal void RemovePermission(ClubAction action)
        {
            // Throw an error if the role doesn't have the permission.
            var permission = _permissions.FirstOrDefault(p => p.Action == action) ?? throw new ArgumentException("This role doesn't have the specifiec permission", nameof(action));
            _permissions.Remove(permission);
        }

        internal void RemovePermissions(IEnumerable<ClubAction> actions)
        {
            foreach (var action in actions)
                RemovePermission(action);
        }

        internal bool HasPermission(ClubAction action)
        {
            return Permissions.Any(p => p.Action == action);
        }
    }
}
