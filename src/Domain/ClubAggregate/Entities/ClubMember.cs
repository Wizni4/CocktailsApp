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
        private readonly List<ClubPermission> _permissions = [];
        public IReadOnlyCollection<ClubPermission> Permissions { get { return _permissions.AsReadOnly(); } }
        private readonly List<ClubRole> _roles = [];
        public IReadOnlyCollection<ClubRole> Roles { get { return _roles.AsReadOnly(); } }
        public Guid UserId { get; }
        internal ClubMember(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId), "UserId cannot be null.");

            UserId = userId;
        }

        internal void AddPermission(ClubPermission permission)
        {
            // Ensure the member doesn't already have this permission.
            if (_permissions.Any(p => p == permission))
                throw new ArgumentException("This member already have specified permission", nameof(permission));

            // Give the permission to the member.
            // FYI: As Permission is a ValueObject, instanciating a new Permission will not create new entry in the database.
            _permissions.Add(permission);
        }

        internal void AddRole(ClubRole role)
        {
            // Ensure the member does not already have this role.
            if (_roles.Any(r => r.Id == role.Id))
                throw new ArgumentException("The member already have this role", nameof(role));

            // Add role to the member
            _roles.Add(role);
        }

        internal void RemovePermission(ClubPermission permission)
        {
            _permissions.Remove(permission);
        }

        internal void RemoveRole(Guid roleId)
        {
            // Get the role, and ensure the member have the role.
            // Throw an error if the memebr doesn have the role.
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The member doesn't have this role", nameof(roleId));
            _roles.Remove(role);
        }

        internal bool HasPermission(ClubPermission permission)
        {
            return Permissions.Any(p => p == permission);
        }
    }
}
