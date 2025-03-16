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
    /// Represents a role within a <see cref="Club"/>, which can have associated permissions.
    /// </summary>
    public class ClubRole : Entity
    {
        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        /// <remarks>
        /// Name can be changed using the <see cref="UpdateName(string)"/> method.
        /// </remarks>
        public string Name { get => _name; }
        private string _name = null!;

        /// <summary>
        /// Gets the <see langword="readonly"/> list of <see cref="ClubPermission"/> of the <see cref="ClubRole"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="ClubPermission"/> can be added within the <see cref="ClubRole"/> using the <see cref="AddPermission(ClubPermission)"/> method.
        /// </para>
        /// <para>
        /// <see cref="ClubPermission"/> can be removed from the <see cref="ClubRole"/> using the <see cref="RemovePermission(ClubPermission)"/> method.
        /// </para>
        /// </remarks>
        public IReadOnlyCollection<ClubPermission> Permissions { get { return _permissions.AsReadOnly(); } }
        private readonly List<ClubPermission> _permissions = [];

        /// <summary>
        /// Creates a new instance of the <see cref="ClubRole"/>
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="name"/> is <see langword="null"/> or <see langword="empty"/>).
        /// </exception>
        internal ClubRole(string name)
        {
            UpdateName(name);
        }

        /// <summary>
        /// Adds <see cref="ClubPermission"/> to the <see cref="ClubRole"/>
        /// </summary>
        /// <param name="permission">The <see cref="ClubPermission"/> to add to the role.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the role already has the <paramref name="permission"/>.
        /// </exception>
        internal void AddPermission(ClubPermission permission)
        {
            // Ensure the role doesn't already have this permission.
            if (_permissions.Any(p => p == permission))
                throw new ArgumentException("The role already has the permission.", nameof(permission));

            // Give the permission to the role.
            // FYI: As Permission is a ValueObject, instanciating a new Permission will not create new entry in the database.
            _permissions.Add(permission);
        }

        /// <summary>
        /// Removes <see cref="ClubPermission"/> from the <see cref="ClubRole"/>
        /// </summary>
        /// <param name="permission">The <see cref="ClubPermission"/> to remove from the role.</param>
        internal void RemovePermission(ClubPermission permission)
        {
            _permissions.Remove(permission);
        }

        /// <summary>
        /// Updates the <see cref="Name"/> of the <see cref="ClubRole"/>.
        /// </summary>
        /// <param name="newName">The new <see cref="Name"/> of the role</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newName"/> is <see langword="null"/> or <see langword="empty"/>
        /// </exception>
        internal void UpdateName(string newName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(newName, nameof(newName));
            _name = newName;

        }

        /// <summary>
        /// Verifies if the role is authorized to perform an action.
        /// </summary>
        /// <param name="permission">The required <see cref="ClubPermission"/> to perform the action.</param>
        /// <returns>
        /// <see langword="true"/> if the role has the required permission, otherwise <see langword="false"/>
        /// </returns>
        internal bool HasPermission(ClubPermission permission)
        {
            return Permissions.Any(p => p == permission);
        }
    }
}
