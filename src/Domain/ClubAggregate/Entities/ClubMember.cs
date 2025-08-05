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
    public sealed class ClubMember : Entity
    {
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
                throw new ArgumentException("UserId cannot be null.");
            UserId = userId;
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
                throw new ArgumentException("The member already has this role.");

            // Add role to the member
            _roles.Add(role);
            Touch();
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
            var role = _roles.FirstOrDefault(r => r.Id == roleId)
                ?? throw new ArgumentException("The member doesn't have this role.");
            _roles.Remove(role);
            Touch();
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
            return (Roles.Any(r => r.HasPermission(permission)));
        }
    }
}
