/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubMember : Entity
    {
        private readonly List<Permission> _permissions = [];
        public IReadOnlyCollection<Permission> Permissions { get { return _permissions.AsReadOnly(); } }
        private readonly List<ClubRole> _roles = [];
        public IReadOnlyCollection<ClubRole> Roles { get { return _roles.AsReadOnly(); } }
        public Guid UserId { get; }
        internal ClubMember(Guid userId)
        {
            UserId = userId;
        }

        internal void AddPermission(ClubAction action)
        {
            // Ensure the member doesn't already have this permission.
            if (_permissions.Any(p => p.Action == action))
                throw new ArgumentException("This member already have specified permission", nameof(action));

            // Give the permission to the member.
            // FYI: As Permission is a ValueObject, instanciating a new Permission will not create new entry in the database.
            _permissions.Add(new Permission(action));
        }

        internal void AddPermissions(IEnumerable<ClubAction> actions)
        {
            foreach (var action in actions)
                AddPermission(action);
        }

        internal void AddRole(ClubRole role)
        {
            // Ensure the member does not already have this role.
            if (_roles.Any(r => r.Id == role.Id))
                throw new ArgumentException("The member already have this role", nameof(role));

            // Add role to the member
            _roles.Add(role);
        }

        internal void AddRoles(IEnumerable<ClubRole> roles)
        {
            foreach (var role in roles)
                AddRole(role);
        }

        internal void RemovePermission(ClubAction action)
        {
            // Throw an error if the role doesn't have the permission.
            var permission = _permissions.FirstOrDefault(p => p.Action == action) ?? throw new ArgumentException("This member doesn't have the specifiec permission", nameof(action));
            _permissions.Remove(permission);
        }

        internal void RemovePermissions(IEnumerable<ClubAction> actions)
        {
            foreach (var action in actions)
                RemovePermission(action);
        }

        internal void RemoveRole(Guid roleId)
        {
            // Get the role, and ensure the member have the role.
            // Throw an error if the memebr doesn have the role.
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The member doesn't have this role", nameof(roleId));
            _roles.Remove(role);
        }

        internal void RemoveRoles(IEnumerable<Guid> roleIds)
        {
            foreach (var roleId in roleIds)
                RemoveRole(roleId);
        }

        internal bool HasPermission(ClubAction action)
        {
            return Permissions.Any(p => p.Action == action);
        }
    }
}
