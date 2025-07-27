/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
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
        /// Gets the <see langword="readonly"/> list of <see cref="ClubPermission"/> of the <see cref="ClubMember"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="ClubPermission"/> can be added within the <see cref="ClubMember"/> using the <see cref="AddPermission(ClubPermission)"/> method.
        /// </para>
        /// <para>
        /// <see cref="ClubPermission"/> can be removed from the <see cref="ClubMember"/> using the <see cref="RemovePermission(ClubPermission)"/> method.
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
        /// Adds <see cref="ClubPermission"/> to the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="permission">The <see cref="ClubPermission"/> to add to the member</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the <see cref="ClubMember"/> already has the <paramref name="permission"/>
        /// </exception>
        internal void AddPermission(ClubPermission permission)
        {
            // Ensure the member doesn't already have this permission.
            if (_permissions.Any(p => p == permission))
                throw new ArgumentException("The member already has specified permission.", nameof(permission));

            // Give the permission to the member.
            _permissions.Add(permission);
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
        /// Removes <see cref="ClubPermission"/> from the <see cref="ClubMember"/>
        /// </summary>
        /// <param name="permission">The <see cref="ClubPermission"/> to remove from the member</param>
        internal void RemovePermission(ClubPermission permission)
        {
            _permissions.Remove(permission);
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
        /// Verifies if the member is authorized to perform an action.
        /// </summary>
        /// <param name="permission">The required <see cref="ClubPermission"/> to perform the action.</param>
        /// <returns>
        /// <see langword="true"/> if the member has the required permission, otherwise <see langword="false"/>
        /// </returns>
        internal bool HasPermission(ClubPermission permission)
        {
            return (Permissions.Any(p => p == permission) ||
                    Roles.Any(r => r.HasPermission(permission)));
        }
    }
}
