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
    public sealed class ClubRole : Entity
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
        /// Gets the <see langword="readonly"/> list of <see cref="ClubPermissionType"/> of the <see cref="ClubRole"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="ClubPermissionType"/> can be added within the <see cref="ClubRole"/> using the <see cref="AddPermission(ClubPermissionType)"/> method.
        /// </para>
        /// <para>
        /// <see cref="ClubPermissionType"/> can be removed from the <see cref="ClubRole"/> using the <see cref="RemovePermission(ClubPermissionType)"/> method.
        /// </para>
        /// </remarks>
        public IReadOnlyCollection<ClubPermission> Permissions { get { return GetPermissions().ToList().AsReadOnly(); } }
        private readonly List<ClubPermission> _permissions = [];

        public bool IsOwnerRole { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ClubRole"/>
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="name"/> is <see langword="null"/> or <see langword="empty"/>).
        /// </exception>
        internal ClubRole(string name, Guid createdBy, bool isOwnerRole = false) : base(createdBy)
        {
            IsOwnerRole = isOwnerRole;
            UpdateName(name, createdBy);
        }

        /// <summary>
        /// Adds <see cref="ClubPermissionType"/> to the <see cref="ClubRole"/>
        /// </summary>
        /// <param name="permission">The <see cref="ClubPermissionType"/> to add to the role.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the role already has the <paramref name="permission"/>.
        /// </exception>
        internal void AddPermission(ClubPermissionType permission, Guid actorId)
        {
            var newPermission = new ClubPermission(permission);

            if (!Enum.IsDefined(permission))
                throw new ArgumentException("Permission is invalid.");

            // Ensure the role doesn't already have this permission.
            if (_permissions.Contains(newPermission))
                throw new ArgumentException($"The role '{Name}' already has the permission '{permission.ToString()}'.");

            // Give the permission to the role.
            // FYI: As Permission is a ValueObject, instanciating a new Permission will not create new entry in the database.
            _permissions.Add(newPermission);
            Touch(actorId);
        }

        /// <summary>
        /// Removes <see cref="ClubPermissionType"/> from the <see cref="ClubRole"/>
        /// </summary>
        /// <param name="permission">The <see cref="ClubPermissionType"/> to remove from the role.</param>
        internal void RemovePermission(ClubPermissionType permission, Guid actorId)
        {
            var newPermission = new ClubPermission(permission);

            if (!Enum.IsDefined(permission))
                throw new ArgumentException("Permission is invalid.");

            if (!_permissions.Contains(newPermission))
                throw new ArgumentException($"The role '{Name}' does not have the permission '{permission.ToString()}'.");

            _permissions.Remove(GetPermission(permission));
            Touch(actorId);
        }

        /// <summary>
        /// Updates the <see cref="Name"/> of the <see cref="ClubRole"/>.
        /// </summary>
        /// <param name="newName">The new <see cref="Name"/> of the role</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newName"/> is <see langword="null"/> or <see langword="empty"/>
        /// </exception>
        internal void UpdateName(string newName, Guid actorId)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("The role name cannot be an empty string or composed entirely of whitespace.");

            _name = newName;
            Touch(actorId);
        }

        /// <summary>
        /// Verifies if the role is authorized to perform an action.
        /// </summary>
        /// <param name="permission">The required <see cref="ClubPermissionType"/> to perform the action.</param>
        /// <returns>
        /// <see langword="true"/> if the role has the required permission, otherwise <see langword="false"/>
        /// </returns>
        internal bool HasPermission(ClubPermissionType permission)
        {
            return Permissions.Any(p => p.Permission == permission);
        }

        private ClubPermission GetPermission(ClubPermissionType permissionType)
        {
            var permission = _permissions.FirstOrDefault(p => new ClubPermissionByTypeSpecification(permissionType).IsSatisfiedBy(p))
                ?? throw new KeyNotFoundException($"Permission '{permissionType.ToString()}' was not found in the '{Name}' role.");

            return permission;
        }

        private IEnumerable<ClubPermission> GetPermissions()
        {
            if (IsOwnerRole)
                return Enum.GetValues<ClubPermissionType>().Select(p => new ClubPermission(p));

            return _permissions;
        }
    }
}
