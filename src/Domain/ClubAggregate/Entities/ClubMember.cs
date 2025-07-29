/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using System.Data;
using System.Security;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    /// <summary>
    /// Represents a meber associated of a <see cref="Club"/>.
    /// </summary>
    public class ClubMember : Entity
    {
        /// <summary>
        /// Gets the <see langword="readonly"/> list of <see cref="ClubPermissionType"/> of the <see cref="ClubMember"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="ClubPermissionType"/> can be added within the <see cref="ClubMember"/> using the <see cref="AddPermission(ClubPermissionType)"/> method.
        /// </para>
        /// <para>
        /// <see cref="ClubPermissionType"/> can be removed from the <see cref="ClubMember"/> using the <see cref="RemovePermission(ClubPermissionType)"/> method.
        /// </para>
        /// </remarks>
        public IReadOnlyCollection<ClubPermission> Permissions { get { return _permissions.AsReadOnly(); } }
        private readonly List<ClubPermission> _permissions = [];

        /// <summary>
        /// Gets the <see langword="readonly"/> list of <see cref="ClubRole"/> of the <see cref="ClubMember"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="ClubRole"/> can be added within the <see cref="ClubMember"/> using the <see cref="AddRole(ClubRole)"/> method.
        /// </para>
        /// <para>
        /// <see cref="ClubRole"/> can be removed from the <see cref="ClubMember"/> using the <see cref="RemoveRole(Guid)"/> method.
        /// </para>
        /// </remarks>
        public IReadOnlyCollection<ClubRole> Roles { get { return _roles.AsReadOnly(); } }
        private readonly List<ClubRole> _roles = [];

        /// <summary>
        /// Gets the unique identifier of the user related to the member.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ClubMember"/>.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="userId"/> is <see cref="Guid.Empty"/>.
        /// </exception>
        internal ClubMember(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId), "UserId cannot be null.");
            UserId = userId;
        }

        /// <summary>
        /// Adds <see cref="ClubPermissionType"/> to the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="permission">The <see cref="ClubPermissionType"/> to add to the member</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the <see cref="ClubMember"/> already has the <paramref name="permission"/>
        /// </exception>
        internal void AddPermission(ClubPermissionType permission)
        {
            // Ensure the member doesn't already have this permission.
            if (_permissions.Any(p => p.Permission == permission))
                throw new ArgumentException("The member already has specified permission.", nameof(permission));

            // Give the permission to the member.
            _permissions.Add(new ClubPermission(permission));
        }

        /// <summary>
        /// Adds a list of <see cref="ClubPermissionType"/> to the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="permissions">The list of <see cref="ClubPermissionType"/> to add to the member</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the <see cref="ClubMember"/> already has the <paramref name="permissions"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="permissions"/> is null or empty
        /// </exception>
        internal void AddPermissions(IEnumerable<ClubPermissionType> permissions)
        {
            if (permissions is null || permissions.Count() == 0)
                throw new ArgumentException("The permission list cannot be null or empty.", nameof(permissions));

            foreach (var permission in permissions)
                AddPermission(permission);
        }

        /// <summary>
        /// Adds <see cref="ClubRole"/> to the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="role">The <see cref="ClubRole"/> to add to the member</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the <see cref="ClubMember"/> already has the <paramref name="role"/>
        /// </exception>
        internal void AddRole(ClubRole role)
        {
            // Ensure the member does not already have this role.
            if (_roles.Any(r => r.Id == role.Id))
                throw new ArgumentException("The member already has this role.", nameof(role));

            // Add role to the member
            _roles.Add(role);
        }

        /// <summary>
        /// Adds a list of <see cref="ClubRole"/> to the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="roles">The list of <see cref="ClubRole"/> to add to the member</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the <see cref="ClubMember"/> already has the <paramref name="roles"/>
        /// </exception>
        ///  <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roles"/> is null or empty
        /// </exception>
        internal void AddRoles(IEnumerable<ClubRole> roles)
        {
            if (roles is null || roles.Count() == 0)
                throw new ArgumentException("The role list cannot be null or empty.", nameof(roles));

            foreach (var role in roles)
                AddRole(role);
        }

        /// <summary>
        /// Removes <see cref="ClubPermissionType"/> from the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="permission">The <see cref="ClubPermissionType"/> to remove from the member</param>
        internal void RemovePermission(ClubPermissionType permission)
        {
            _permissions.Remove(new ClubPermission(permission));
        }

        /// <summary>
        /// Removes a list of <see cref="ClubPermissionType"/> from the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="permissions">The list of <see cref="ClubPermissionType"/> to remove from the member</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="permissions"/> is null or empty
        /// </exception>
        internal void RemovePermissions(IEnumerable<ClubPermissionType> permissions)
        {
            if (permissions is null || permissions.Count() == 0)
                throw new ArgumentException("The permission list cannot be null or empty.", nameof(permissions));

            foreach (var permission in permissions)
                RemovePermission(permission);
        }

        /// <summary>
        /// Removes <see cref="ClubRole"/> from the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="roleId">The <see cref="ClubRole.Id"/> to remove from the member</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the member doesn't have the <paramref name="roleId"/>.
        /// </exception>
        internal void RemoveRole(Guid roleId)
        {
            // Get the role, and ensure the member have the role.
            // Throw an error if the memebr doesn have the role.
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The member doesn't have this role.", nameof(roleId));
            _roles.Remove(role);
        }

        /// <summary>
        /// Removes a list of <see cref="ClubRole"/> from the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="roleIds">The list of <see cref="ClubRole.Id"/> to remove from the member</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the member doesn't have the <paramref name="roleId"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleIds"/> is null or empty
        /// </exception>
        internal void RemoveRoles(IEnumerable<Guid> roleIds)
        {
            if (roleIds is null || roleIds.Count() == 0)
                throw new ArgumentException("The role list cannot be null or empty.", nameof(roleIds));

            foreach (var roleId in roleIds)
                RemoveRole(roleId);
        }

        /// <summary>
        /// Verifies if the member is authorized to perform an action.
        /// </summary>
        /// <param name="permission">The required <see cref="ClubPermissionType"/> to perform the action.</param>
        /// <returns>
        /// <see langword="true"/> if the member has the required permission, otherwise <see langword="false"/>
        /// </returns>
        internal bool HasPermission(ClubPermissionType permission)
        {
            return (Permissions.Any(p => p.Permission == permission) ||
                    Roles.Any(r => r.HasPermission(permission)));
        }
    }
}
