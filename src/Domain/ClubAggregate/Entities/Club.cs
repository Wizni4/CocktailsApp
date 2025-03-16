/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    /// <summary>
    /// Represents a Club entity, which is an aggregate root in the domain model.
    ///
    /// </summary>
    /// <remarks>
    /// A Club has foolowing attributes:
    /// <list type="bullet">
    /// <item>
    ///     <term><see cref="Address"/></term>
    ///     <description><see langword="required"/> The address of the club</description>
    /// </item>
    /// <item>
    ///     <term><see cref="Cocktails"/></term>
    ///     <description>List of <see cref="ClubCocktail"/> available in the club</description>
    /// </item>
    /// <item>
    ///     <term><see cref="Description"/></term>
    ///     <description><see langword="required"/> The description of the club</description>
    /// </item>
    /// <item>
    ///     <term><see cref="Name"/></term>
    ///     <description><see langword="required"/> The name of the club</description>
    /// </item>
    /// <item>
    ///     <term><see cref="Members"/></term>
    ///     <description><see cref="ClubMember"/> of the club</description>
    /// </item>
    /// <item>
    ///     <term><see cref="Owner"/></term>
    ///     <description>The <see cref="ClubMember"/>, owner of the club</description>
    /// </item>
    /// <item>
    ///     <term><see cref="Roles"/></term>
    ///     <description>List of <see cref="ClubRole"/> available in the club</description>
    /// </item>
    /// <item>
    ///     <term><see cref="Visibility"/></term>
    ///     <description><see langword="required"/> <see cref="ClubVisibility"/> of the club</description>
    /// </item>
    /// </list> 
    /// </remarks>
    public class Club : Entity, IAggregateRoot
    {
        /// <summary>
        /// Gets the <see langword="readonly"/> address of the club.
        /// </summary>
        /// <remarks>
        /// Address can be changed using the <see cref="UpdateAddress(Address?, Guid)"/> method.
        /// </remarks>
        public Address? Address { get; private set; }


        /// <summary>
        /// Gets the <see langword="readonly"/> collection of <see cref="ClubCocktail"/> available in the club.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="ClubCocktail"/> can be added to the <see cref="Club"/> using the <see cref="AddCocktail(Guid, Guid)"/> method.
        /// </para>
        /// <para>
        /// <see cref="ClubCocktail"/> can be removed from the <see cref="Club"/> using the <see cref="RemoveCocktail(Guid, Guid)"/> method.
        /// </para>
        /// </remarks>
        public IReadOnlyCollection<ClubCocktail> Cocktails { get => _cocktails.AsReadOnly(); }
        private readonly List<ClubCocktail> _cocktails = [];

        /// <summary>
        /// Gets the <see langword="readonly"/> description of the club.
        /// </summary>
        /// <remarks>
        /// Description can be changed using the <see cref="UpdateDescription(string?, Guid)"/> method.
        /// </remarks>
        public string Description { get => _description; }
        private string _description = null!;

        /// <summary>
        /// Gets the <see langword="readonly"/> name of the club.
        /// </summary>
        /// <remarks>
        /// Name can be changed using the <see cref="UpdateName(string?, Guid)"/> method.
        /// </remarks>
        public string Name { get => _name; }
        private string _name = null!;

        /// <summary>
        /// Gets the <see langword="readonly"/> collection of <see cref="ClubMember"/> of the club.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="ClubMember"/> can be added to the <see cref="Club"/> using the <see cref="AddMember(Guid, Guid)"/> method.
        /// </para>
        /// <para>
        /// <see cref="ClubMember"/> can be removed from the <see cref="Club"/> using the <see cref="RemoveMember(Guid, Guid)"/> method.
        /// </para>
        /// </remarks>
        public IReadOnlyCollection<ClubMember> Members { get => _members.AsReadOnly(); }
        private readonly List<ClubMember> _members = [];

        /// <summary>
        /// Gets the <see langword="readonly"/> owner of the club.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Owner is automatically added as a <see cref="ClubMember"/> upon the creation of the <see cref="Club"/>.
        /// </para>
        /// <para>
        /// Owner can be changed using the <see cref="UpdateOwner(Guid, Guid)"/> method.
        /// </para>
        /// </remarks>
        public ClubMember Owner { get; private set; }

        /// <summary>
        /// Gets the <see langword="readonly"/> list of <see cref="ClubRole"/> of the club.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="ClubRole"/> can be created within the <see cref="Club"/> using the <see cref="CreateRole(string, Guid)"/> method.
        /// </para>
        /// <para>
        /// <see cref="ClubMember"/> can be deleted from the <see cref="Club"/> using the <see cref="DeleteRole(Guid, Guid)"/> method.
        /// </para>
        /// </remarks>
        public IReadOnlyCollection<ClubRole> Roles { get => _roles.AsReadOnly(); }
        private readonly List<ClubRole> _roles = [];

        /// <summary>
        /// Gets the visibility of the club.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Available values are:
        /// <list type="bullet">
        /// <item><see cref="ClubVisibility.Public"/></item>
        /// <item><see cref="ClubVisibility.Private"/></item>
        /// </list>
        /// </para>
        /// <para>
        /// Visibility can be changed using the <see cref="UpdateVisibility(ClubVisibility, Guid)"/> method.
        /// </para>
        /// </remarks>
        public ClubVisibility Visibility { get; private set; }

        /// <summary>
        /// Creates a new instance of a <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <see cref="Owner"/> is automatically added as a <see cref="ClubMember"/> upon the creation of the <see cref="Club"/>.
        /// </para>
        /// <para>
        /// Following parameters are required and must be not <see langword="null"/> nor empty:
        /// <list type="bullet">
        /// <item><paramref name="address"/></item>
        /// <item><paramref name="description"/></item>
        /// <item><paramref name="name"/></item>
        /// <item><paramref name="ownerId"/></item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <param name="address">The address of the club.</param>
        /// <param name="description">The description of the club.</param>
        /// <param name="name">The name of the club.</param>
        /// <param name="ownerId">The ID of the owner of the club.</param>
        /// <param name="visibility">The visibility of the club.</param>
        /// <exception cref = "ArgumentNullException">
        /// Thrown when any of the provided arguments are invalid (<see langword="null"/> or <see langword="empty"/>).
        /// </exception>
        internal Club(Address? address, string? description, string? name, Guid ownerId, ClubVisibility visibility)
        {
            // Create a new Club member that will be the owner of the club.
            Owner = new ClubMember(ownerId);

            // Add the Owner as a Member of the club
            _members.Add(Owner);

            // Set Club characteristics (done by the owner -> he has all permissions):
            // - Address
            // - Name
            // - Visibility
            UpdateAddress(address, ownerId);
            UpdateDescription(description, ownerId);
            UpdateName(name, ownerId);
            UpdateVisibility(visibility, ownerId);
        }

        /// <summary>
        /// Adds a <see cref="ClubCocktail"/> to the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.AddCocktail"/> permission can perform this action.
        /// </remarks>
        /// <param name="cocktailId">The <see cref="ClubCocktail.CocktailId"/> of the cocktail to add.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="cocktailId"/> is already available in the club.
        /// </exception>
        public void AddCocktail(Guid cocktailId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.AddCocktail);

            // To avoid duplicates, make sure the cocktail is not already in the club
            if (_cocktails.Any(cc => cc.CocktailId == cocktailId))
                throw new ArgumentException("This cocktail is already in the club.", nameof(cocktailId));

            _cocktails.Add(new ClubCocktail(cocktailId));
        }

        /// <summary>
        /// Adds a <see cref="ClubMember"/> to the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.AddMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="memberId">The <see cref="ClubMember.UserId"/> of the member to add.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="memberId"/> is already in the club.
        /// </exception>
        public void AddMember(Guid memberId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.AddMember);

            // Verify that the specified user is not already ine the club. If yes throw an argument exception.
            if (_members.Any(m => m.UserId == memberId))
                throw new ArgumentException("This member is already in the club.", nameof(memberId));

            // Create and add a new member to the club.
            _members.Add(new ClubMember(memberId));
        }

        /// <summary>
        /// Adds a <see cref="ClubPermission"/> to a <see cref="ClubMember"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.AddPermissionToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="memberId">The <see cref="ClubMember.UserId"/> of the member.</param>
        /// <param name="permission">The <see cref="ClubPermission"/> to add to the member.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="memberId"/> is not in the club, or when the <paramref name="memberId"/> already has the <paramref name="permission"/>.
        /// </exception>
        public void AddPermissionToMember(Guid memberId, ClubPermission permission, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.AddPermissionToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));

            // Add permission to the member
            // Throw an exception if the member already have this permission.
            member.AddPermission(permission);
        }

        /// <summary>
        /// Adds a <see cref="ClubPermission"/> to a <see cref="ClubRole"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.AddRolePermission"/> permission can perform this action.
        /// </remarks>
        /// <param name="roleId">The <see cref="ClubRole.Id"/> of the role.</param>
        /// <param name="permission">The <see cref="ClubPermission"/> to add to the role.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        public void AddRolePermission(Guid roleId, ClubPermission permission, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.AddRolePermission);

            // Throw an exception if specified role does not exist in the club
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));

            // Add the permission to the role
            role.AddPermission(permission);
        }

        /// <summary>
        /// Adds a <see cref="ClubRole"/> to a <see cref="ClubMember"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.AddRoleToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="memberId">The <see cref="ClubMember.UserId"/> of the member.</param>
        /// <param name="roleId">The <see cref="ClubRole.Id"/> to add to the member.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item>The <paramref name="memberId"/> is not in the club.</item>
        /// <item>The <paramref name="roleId"/> is not in the club.</item>
        /// <item>The <paramref name="memberId"/> already has the <paramref name="roleId"/>.</item> 
        /// </list>
        /// </exception>
        public void AddRoleToMember(Guid memberId, Guid roleId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.AddRoleToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));

            // Throw an exception if the role does not exist in the club
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The role doesn't exists in the club.", nameof(memberId));

            // Add role to member
            member.AddRole(role);
        }

        /// <summary>
        /// Creates a new <see cref="ClubRole"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.CreateRole"/> permission can perform this action.
        /// </remarks>
        /// <param name="roleName">The <see cref="ClubRole.Name"/> of the role.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleName"/> is already in the club.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="roleName"/> is <see langword="null"/> or <see langword="empty"/>.
        /// </exception>
        public void CreateRole(string roleName, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.CreateRole);

            // Ensure the role name is unique within the club to avoid duplicates.
            if (_roles.Any(g => g.Name == roleName))
                throw new ArgumentException("This role name already exists", nameof(roleName));

            _roles.Add(new ClubRole(roleName));
        }

        /// <summary>
        /// Deletes a <see cref="ClubRole"/> from the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.DeleteRole"/> permission can perform this action.
        /// </para>
        /// <para>
        /// The <see cref="ClubRole"/> is also removed from all <see cref="ClubMember"/> instances within the <see cref="Club"/>.
        /// </para>
        /// </remarks>
        /// <param name="roleId">The <see cref="ClubRole.Id"/> of the role.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        public void DeleteRole(Guid roleId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.DeleteRole);

            // Ensure the role name is unique within the club to avoid duplicates.
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));

            // Remove role for all members
            foreach (var member in _members)
                if (member.Roles.Any(r => r.Id == roleId))
                    member.RemoveRole(roleId);

            // Remove role from the club
            _roles.Remove(role);
        }

        /// <summary>
        /// Removes a <see cref="ClubCocktail"/> from the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.RemoveCocktail"/> permission can perform this action.
        /// </remarks>
        /// <param name="cocktailId">The <see cref="ClubCocktail.Id"/> of the cocktail to remove.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="cocktailId"/> is not in the club.
        /// </exception>
        public void RemoveCocktail(Guid cocktailId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.RemoveCocktail);

            // Throw an error if the club doesn have the cocktail.
            var cocktail = _cocktails.FirstOrDefault(c => c.Id == cocktailId) ?? throw new ArgumentException("The cocktail could not be found in the club.", nameof(cocktailId));
            _cocktails.Remove(cocktail);
        }

        /// <summary>
        /// Removes a <see cref="ClubMember"/> from the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.RemoveMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="memberId">The <see cref="ClubMember.Id"/> of the member to remove.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="memberId"/> is not in the club.
        /// </exception>
        public void RemoveMember(Guid memberId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.RemoveMember);

            // Throw an error if the member is not in the club.
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));
            _members.Remove(member);
        }

        /// <summary>
        /// Removes a <see cref="ClubPermission"/> from a <see cref="ClubMember"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.RemovePermissionToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="memberId">The <see cref="ClubMember.Id"/> of the member.</param>
        /// <param name="permision">The <see cref="ClubPermission"/> to remove.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="memberId"/> is not in the club.
        /// </exception>
        public void RemovePermissionToMember(Guid memberId, ClubPermission permision, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.RemovePermissionToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));

            // Remove permission to the member
            member.RemovePermission(permision);
        }

        /// <summary>
        /// Removes a <see cref="ClubPermission"/> from a <see cref="ClubRole"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.RemoveRolePermission"/> permission can perform this action.
        /// </remarks>
        /// <param name="roleId">The <see cref="ClubRole.Id"/> of the role.</param>
        /// <param name="permision">The <see cref="ClubPermission"/> to remove.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        public void RemoveRolePermission(Guid roleId, ClubPermission permision, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.RemoveRolePermission);

            // Throw an error if the role is not in the club.
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));
            role.RemovePermission(permision);
        }

        /// <summary>
        /// Removes a <see cref="ClubRole"/> from a <see cref="ClubMember"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.RemoveRoleToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="memberId">The <see cref="ClubMember"/> of the member.</param>
        /// <param name="roleId">The <see cref="ClubRole.Id"/> of the role.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="memberId"/> is not in the club, or when the <paramref name="memberId"/> doesn't have the <paramref name="roleId"/>.
        /// </exception>
        public void RemoveRoleToMember(Guid memberId, Guid roleId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.RemoveRoleToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));

            // Remove role to member
            member.RemoveRole(roleId);
        }

        /// <summary>
        /// Updates the <see cref="Address"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.ChangeAddress"/> permission can perform this action.
        /// </remarks>
        /// <param name="newAddress">The new <see cref="Address"/> of the club.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newAddress"/> is <see langword="null"/> or <see langword="empty"/>.
        /// </exception>
        public void UpdateAddress(Address? newAddress, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeAddress);
            ArgumentNullException.ThrowIfNull(newAddress, nameof(newAddress));
            Address = newAddress;

        }

        /// <summary>
        /// Updates the <see cref="Description"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.ChangeDescription"/> permission can perform this action.
        /// </remarks>
        /// <param name="newDescription">The new <see cref="Description"/> of the club.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newDescription"/> is <see langword="null"/> or <see langword="empty"/>.
        /// </exception>
        public void UpdateDescription(string? newDescription, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeDescription);
            ArgumentException.ThrowIfNullOrWhiteSpace(newDescription, nameof(newDescription));
            _description = newDescription;
        }

        /// <summary>
        /// Updates the <see cref="Name"> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.ChangeName"/> permission can perform this action.
        /// </remarks>
        /// <param name="newName">The new <see cref="Name"/> of the club.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newName"/> is <see langword="null"/> or <see langword="empty"/>.
        /// </exception>
        public void UpdateName(string? newName, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeName);
            ArgumentException.ThrowIfNullOrWhiteSpace(newName, nameof(newName));
            _name = newName;
        }

        /// <summary>
        /// Updates the <see cref="Owner"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only the <paramref name="userId"/> who is the <see cref="Owner"/> of the <see cref="Club"/> can perform this action.
        /// </remarks>
        /// <param name="newOwnerId">The <see cref="ClubMember.Id"/> of the new owner.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="newOwnerId"/> is not in the club.
        /// </exception>
        public void UpdateOwner(Guid newOwnerId, Guid userId)
        {
            if (Owner.UserId != userId)
                throw new UnauthorizedAccessException("Only the Owner of the club can change ownership of the club.");

            // Get the member of the club that will be the new owner
            var member = _members.FirstOrDefault(m => m.Id == newOwnerId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(newOwnerId));

            // Replace owner by the member
            Owner = member;
        }

        /// <summary>
        /// Updates the <see cref="ClubRole.Name"/> of a <see cref="ClubRole"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.ChangeRoleName"/> permission can perform this action.
        /// </remarks>
        /// <param name="newName">The new name of the role.</param>
        /// <param name="roleId">The <see cref="ClubRole.Id"/> of the role.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newName"/> is <see langword="null"/> or <see langword="empty"/>
        /// </exception>
        public void UpdateRoleName(string newName, Guid roleId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeRoleName);

            // Throw an exception if the member does not exist in the club
            var role = _roles.FirstOrDefault(m => m.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));
            role.UpdateName(newName);
        }

        /// <summary>
        /// Updates the <see cref="Visibility"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="userId"/> with the <see cref="ClubPermission.ChangeVisibility"/> permission can perform this action.
        /// </remarks>
        /// <param name="visibility">The new visibility of the club.</param>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        public void UpdateVisibility(ClubVisibility visibility, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeVisibility);
            Visibility = visibility;
        }

        /// <summary>
        /// Validates if a user is authorized to perform an action.
        /// </summary>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <param name="permission">The required <see cref="ClubPermission"/> to perform the action</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="userId"/> is not authorized to perform the action.
        /// </exception>
        private void ValidateUserPermission(Guid userId, ClubPermission permission)
        {
            if (!IsMemberAuthorized(userId, permission))
                throw new UnauthorizedAccessException($"User does not have permission to {permission}");

        }

        /// <summary>
        /// Verifies if a user is authorized to perform an action.
        /// </summary>
        /// <remarks>
        /// The user is authorized if any of the following conditions are met:
        /// <list type="bullet">
        /// <item>The user has the permission as a <see cref="ClubMember"/>.</item>
        /// <item>The user has the permission through a <see cref="ClubRole"/>.</item>
        /// <item>The user is the <see cref="Owner"/> of the <see cref="Club"/>.</item>
        /// </list>
        /// </remarks>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the user performing the action.</param>
        /// <param name="permission">The required <see cref="ClubPermission"/> to perform the action.</param>
        /// <returns>
        /// <see langword="true"/> if the user has the required permission, otherwise <see langword="false"/>.
        /// </returns>
        private bool IsMemberAuthorized(Guid userId, ClubPermission permission)
        {
            // Get the member based on the specified user ID
            var member = _members.FirstOrDefault(m => m.UserId == userId);

            // Return false if the user is not part of the club
            if (member is null)
                return false;

            // Return true if the user has permission to perform the action.
            // A user can have permission if:
            // - They have the permission as a member
            // - They have permission through a role
            // - He's the owner of club
            return (member.HasPermission(permission) ||
                    member.Roles.Any(r => r.HasPermission(permission)) ||
                    Owner.Id == member.Id);
                
        }
    }
}
