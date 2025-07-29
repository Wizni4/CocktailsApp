/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

using System.Data;
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
    public class Club : AggregateRoot, IAggregateRoot
    {
        /// <summary>
        /// Gets the <see langword="readonly"/> address of the club.
        /// </summary>
        /// <remarks>
        /// Address can be changed using the <see cref="UpdateAddress(Address?, Guid)"/> method.
        /// </remarks>
        public Address? Address { get => _address; }
        private Address? _address = null!;

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

#pragma warning disable CS8618
        private Club() { } // <----- EF forced me
#pragma warning restore CS8618

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

            // Create a new Owner role
            var ownerRole = new ClubRole(
                "Owner",
                Enum.GetValues<ClubPermissionType>(),
                true
            );

            // Add the owner role to both: club & owner (member)
            Owner.AddRole(ownerRole);
            _roles.Add(ownerRole);

            // Set Club characteristics (done by the owner -> he has all permissions):
            // - Address
            // - Name
            // - Visibility
            UpdateAddress(address, Owner.Id);
            UpdateDescription(description, Owner.Id);
            UpdateName(name, Owner.Id);
            UpdateVisibility(visibility, Owner.Id);
        }

        /// <summary>
        /// Adds a <see cref="ClubCocktail"/> to the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddCocktail"/> permission can perform this action.
        /// </remarks>
        /// <param name="cocktailId">The <see cref="ClubCocktail.CocktailId"/> of the cocktail to add.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="cocktailId"/> is already available in the club.
        /// </exception>
        public void AddCocktail(Guid cocktailId, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.AddCocktail);

            // To avoid duplicates, make sure the cocktail is not already in the club
            if (_cocktails.Any(cc => cc.CocktailId == cocktailId))
                throw new ArgumentException("This cocktail is already in the club.", nameof(cocktailId));

            _cocktails.Add(new ClubCocktail(cocktailId));
        }

        /// <summary>
        /// Adds multiple <see cref="ClubCocktail"/>s to the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddCocktail"/> permission can perform this action.
        /// </remarks>
        /// <param name="cocktailIds">The list of <see cref="ClubCocktail.CocktailId"/> of the cocktails to add.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when one or more of the <paramref name="cocktailIds"/> are already available in the club.
        /// </exception>
        public void AddCocktails(IEnumerable<Guid> cocktailIds, Guid actorId)
        {
            if (cocktailIds is null || cocktailIds.Count() == 0)
                throw new ArgumentException("The cocktail ID list cannot be null or empty.", nameof(cocktailIds));

            foreach (var cocktail in cocktailIds)
                AddCocktail(cocktail, actorId);
        }

        /// <summary>
        /// Adds a <see cref="ClubMember"/> to the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="userId">The <see cref="ClubMember.UserId"/> of the member to add.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="userId"/> is already in the club.
        /// </exception>
        public ClubMember AddMember(Guid userId, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.AddMember);

            // Verify that the specified user is not already ine the club. If yes throw an argument exception.
            if (_members.Any(m => m.UserId == userId))
                throw new ArgumentException("This user is already a member of the club.", nameof(userId));

            // Create and add a new member to the club.
            var member = new ClubMember(userId);
            _members.Add(member);

            return member;
        }

        /// <summary>
        /// Adds multipke <see cref="ClubMember"/> to the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="userIds">The list of <see cref="ClubMember.UserId"/> of the members to add.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="userId"/> is already in the club.
        /// </exception>
        public void AddMembers(IEnumerable<Guid> userIds, Guid actorId)
        {
            if (userIds is null || userIds.Count() == 0)
                throw new ArgumentException("The user ID list cannot be null or empty.", nameof(userIds));

            foreach (var userId in userIds)
                AddMember(userId, actorId);
        }

        /// <summary>
        /// Adds a <see cref="ClubPermissionType"/> to a <see cref="ClubMember"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddPermissionToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberId">The <see cref="Entity.Id"/> of the member.</param>
        /// <param name="permission">The <see cref="ClubPermissionType"/> to add to the member.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="clubMemberId"/> is not in the club, or when the <paramref name="clubMemberId"/> already has the <paramref name="permission"/>.
        /// </exception>
        public void AddPermissionToMember(Guid clubMemberId, ClubPermissionType permission, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.AddPermissionToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == clubMemberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(clubMemberId));

            // Add permission to the member
            // Throw an exception if the member already have this permission.
            member.AddPermission(permission);
        }

        /// <summary>
        /// Adds a list of <see cref="ClubPermissionType"/> to a <see cref="ClubMember"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddPermissionToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberId">The <see cref="Entity.Id"/> of the member.</param>
        /// <param name="permissions">The list of <see cref="ClubPermissionType"/> to add to the member.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="clubMemberId"/> is not in the club, or when the <paramref name="clubMemberId"/> already has the <paramref name="permission"/>.
        /// </exception>
        public void AddPermissionsToMember(Guid clubMemberId, IEnumerable<ClubPermissionType> permissions, Guid actorId)
        {
            if (permissions is null || permissions.Count() == 0)
                throw new ArgumentException("The permission list cannot be null or empty.", nameof(permissions));

            foreach (var permission in permissions)
                AddPermissionToMember(clubMemberId, permission, actorId);
        }

        /// <summary>
        /// Adds a <see cref="ClubPermissionType"/> to a <see cref="ClubRole"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddRolePermission"/> permission can perform this action.
        /// </remarks>
        /// <param name="roleId">The <see cref="Entity.Id"/> of the role.</param>
        /// <param name="permission">The <see cref="ClubPermissionType"/> to add to the role.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        public void AddPermissionToRole(Guid roleId, ClubPermissionType permission, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.AddRolePermission);

            // Throw an exception if specified role does not exist in the club
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));

            // Add the permission to the role
            role.AddPermission(permission);
        }

        /// <summary>
        /// Adds a list of <see cref="ClubPermissionType"/> to a <see cref="ClubRole"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddRolePermission"/> permission can perform this action.
        /// </remarks>
        /// <param name="roleId">The <see cref="Entity.Id"/> of the role.</param>
        /// <param name="permissions">The list of <see cref="ClubPermissionType"/> to add to the role.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        public void AddPermissionsToRole(Guid roleId, IEnumerable<ClubPermissionType> permissions, Guid actorId)
        {
            if (permissions is null || permissions.Count() == 0)
                throw new ArgumentException("The permission list cannot be null or empty.", nameof(permissions));

            foreach (var permission in permissions)
                AddPermissionToRole(roleId, permission, actorId);
        }

        /// <summary>
        /// Adds a <see cref="ClubRole"/> to a <see cref="ClubMember"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddRoleToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberId">The <see cref="Entity.Id"/> of the member.</param>
        /// <param name="roleId">The <see cref="ClubRole.Id"/> to add to the member.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item>The <paramref name="clubMemberId"/> is not in the club.</item>
        /// <item>The <paramref name="roleId"/> is not in the club.</item>
        /// <item>The <paramref name="clubMemberId"/> already has the <paramref name="roleId"/>.</item> 
        /// </list>
        /// </exception>
        public void AddRoleToMember(Guid clubMemberId, Guid roleId, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.AddRoleToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == clubMemberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(clubMemberId));

            // Throw an exception if the role does not exist in the club
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));

            // Add role to member
            member.AddRole(role);
        }

        /// <summary>
        /// Adds a list of <see cref="ClubRole"/> to a <see cref="ClubMember"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.AddRoleToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberId">The <see cref="Entity.Id"/> of the member.</param>
        /// <param name="roleIds">The list of <see cref="ClubRole.Id"/> to add to the member.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item>The <paramref name="clubMemberId"/> is not in the club.</item>
        /// <item>The <paramref name="roleId"/> is not in the club.</item>
        /// <item>The <paramref name="clubMemberId"/> already has the <paramref name="roleId"/>.</item> 
        /// </list>
        /// </exception>
        public void AddRolesToMember(Guid clubMemberId, IEnumerable<Guid> roleIds, Guid actorId)
        {
            if (roleIds is null || roleIds.Count() == 0)
                throw new ArgumentException("The role ID list cannot be null or empty.", nameof(roleIds));

            foreach (var roleId in roleIds)
                AddRoleToMember(clubMemberId, roleId, actorId);
        }

        /// <summary>
        /// Creates a new <see cref="ClubRole"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.CreateRole"/> permission can perform this action.
        /// </remarks>
        /// <param name="roleName">The <see cref="ClubRole.Name"/> of the role.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleName"/> is already in the club.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="roleName"/> is <see langword="null"/> or <see langword="empty"/>.
        /// </exception>
        public ClubRole CreateRole(string roleName, Guid actorId)
        {

            return CreateRole(roleName, null, actorId);
        }

        public ClubRole CreateRole(string roleName, IEnumerable<ClubPermissionType>? permissions, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.CreateRole);

            // Ensure the role name is unique within the club to avoid duplicates.
            if (_roles.Any(g => g.Name == roleName))
                throw new ArgumentException("This role name already exists.", nameof(roleName));

            var role = permissions == null
                ? new ClubRole(roleName)
                : new ClubRole(roleName, permissions);

            _roles.Add(role);

            return role;
        }

        /// <summary>
        /// Delete the <see cref="Club"/>
        /// <para>
        /// Only the <see cref="Owner"/> of the club can perform this action.
        /// </para>
        /// </summary>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        public void DeleteClub(Guid actorId)
        {
            if (!this.IsOwner(actorId))
                throw new UnauthorizedAccessException("Only the Owner of the club can delete the club.");

            // Raise the event
            AddDomainEvent(new ClubDeletedEvent(actorId));
        }

        /// <summary>
        /// Deletes a <see cref="ClubRole"/> from the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.DeleteRole"/> permission can perform this action.
        /// </para>
        /// <para>
        /// The <see cref="ClubRole"/> is also removed from all <see cref="ClubMember"/> instances within the <see cref="Club"/>.
        /// </para>
        /// </remarks>
        /// <param name="roleId">The <see cref="Entity.Id"/> of the role.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        public void DeleteRole(Guid roleId, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.DeleteRole);

            // Ensure the role name is unique within the club to avoid duplicates.
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));

            // Throw an error if the role is the Owner role
            if (role.IsOwnerRole)
                throw new UnauthorizedAccessException("Owner role can't be removed from the club.");

            // Remove role for all members
            foreach (var member in _members)
                if (member.Roles.Any(r => r.Id == roleId))
                    member.RemoveRole(roleId);

            // Remove role from the club
            _roles.Remove(role);
        }

        /// <summary>
        /// Deletes a list of <see cref="ClubRole"/> from the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.DeleteRole"/> permission can perform this action.
        /// </para>
        /// <para>
        /// The <see cref="ClubRole"/> is also removed from all <see cref="ClubMember"/> instances within the <see cref="Club"/>.
        /// </para>
        /// </remarks>
        /// <param name="roleIds">The list of <see cref="Entity.Id"/> to delete.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        public void DeleteRoles(IEnumerable<Guid> roleIds, Guid actorId)
        {
            if (roleIds is null || roleIds.Count() == 0)
                throw new ArgumentException("The role ID list cannot be null or empty.", nameof(roleIds));

            foreach (var roleId in roleIds)
                DeleteRole(roleId, actorId);
        }

        /// <summary>
        /// Removes a <see cref="ClubCocktail"/> from the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemoveCocktail"/> permission can perform this action.
        /// </remarks>
        /// <param name="cocktailId">The <see cref="Entity.Id"/> or the <see cref="ClubCocktail.CocktailId"/> of the cocktail to remove.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="cocktailId"/> is not in the club.
        /// </exception>
        public void RemoveCocktail(Guid cocktailId, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.RemoveCocktail);

            // Throw an error if the club doesn have the cocktail.
            var cocktail = _cocktails.FirstOrDefault(c => c.Id == cocktailId || c.CocktailId == cocktailId) ?? throw new ArgumentException("The cocktail could not be found in the club.", nameof(cocktailId));
            _cocktails.Remove(cocktail);
        }

        /// <summary>
        /// Removes a list of <see cref="ClubCocktail"/> from the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemoveCocktail"/> permission can perform this action.
        /// </remarks>
        /// <param name="cocktailIds">The list of <see cref="Entity.Id"/> or the <see cref="ClubCocktail.CocktailId"/> to remove.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="cocktailId"/> is not in the club.
        /// </exception>
        public void RemoveCocktails(IEnumerable<Guid> cocktailIds, Guid actorId)
        {
            if (cocktailIds is null || cocktailIds.Count() == 0)
                throw new ArgumentException("The cocktail ID list cannot be null or empty.", nameof(cocktailIds));

            foreach (var cocktailId in cocktailIds)
                RemoveCocktail(cocktailId, actorId);
        }

        /// <summary>
        /// Removes a <see cref="ClubMember"/> from the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemoveMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberId">The <see cref="Entity.Id"/> of the member to remove.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="clubMemberId"/> is not in the club.
        /// </exception>
        public void RemoveMember(Guid clubMemberId, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.RemoveMember);

            // Throw an error if the member we want to remove is the owner of the club
            if (IsOwner(clubMemberId))
                throw new UnauthorizedAccessException("The owner of the club can't be remove from the club");

            // Throw an error if the member is not in the club.
            var member = _members.FirstOrDefault(m => m.Id == clubMemberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(clubMemberId));

            // Throw an error if the member we want to remove is the member performing the action
            if (member.Id == actorId || member.UserId == actorId)
                throw new UnauthorizedAccessException("A member can't remove him self from a club");

            _members.Remove(member);
        }

        /// <summary>
        /// Removes a list of <see cref="ClubMember"/> from the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemoveMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberIds">The list of <see cref="Entity.Id"/> to remove.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="clubMemberId"/> is not in the club.
        /// </exception>
        public void RemoveMembers(IEnumerable<Guid> clubMemberIds, Guid actorId)
        {
            if (clubMemberIds is null || clubMemberIds.Count() == 0)
                throw new ArgumentException("The member ID list cannot be null or empty.", nameof(clubMemberIds));

            foreach (var memberId in clubMemberIds)
                RemoveMember(memberId, actorId);
        }

        /// <summary>
        /// Removes a <see cref="ClubPermissionType"/> from a <see cref="ClubMember"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemovePermissionToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberId">The <see cref="Entity.Id"/> of the member.</param>
        /// <param name="permision">The <see cref="ClubPermissionType"/> to remove.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="clubMemberId"/> is not in the club.
        /// </exception>
        public void RemovePermissionToMember(Guid clubMemberId, ClubPermissionType permision, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.RemovePermissionToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == clubMemberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(clubMemberId));

            // Remove permission to the member
            member.RemovePermission(permision);
        }

        /// <summary>
        /// Removes a list of <see cref="ClubPermissionType"/> from a <see cref="ClubMember"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemovePermissionToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberId">The <see cref="Entity.Id"/> of the member.</param>
        /// <param name="permisions">The list of <see cref="ClubPermissionType"/> to remove.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="clubMemberId"/> is not in the club.
        /// </exception>
        public void RemovePermissionsToMember(Guid clubMemberId, IEnumerable<ClubPermissionType> permisions, Guid actorId)
        {
            if (permisions is null || permisions.Count() == 0)
                throw new ArgumentException("The permission list cannot be null or empty.", nameof(permisions));

            foreach (var permission in permisions)
                RemovePermissionToMember(clubMemberId, permission, actorId);
        }

        /// <summary>
        /// Removes a <see cref="ClubPermissionType"/> from a <see cref="ClubRole"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemoveRolePermission"/> permission can perform this action.
        /// </remarks>
        /// <param name="roleId">The <see cref="Entity.Id"/> of the role.</param>
        /// <param name="permision">The <see cref="ClubPermissionType"/> to remove.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        public void RemovePermissionToRole(Guid roleId, ClubPermissionType permision, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.RemoveRolePermission);

            // Throw an error if the role is not in the club.
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));

            // Throw an error if the role is the Owner role
            if (role.IsOwnerRole)
                throw new UnauthorizedAccessException("Permissions cannot be removed from the Owner role");

            role.RemovePermission(permision);
        }

        /// <summary>
        /// Removes a list of <see cref="ClubPermissionType"/> from a <see cref="ClubRole"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemoveRolePermission"/> permission can perform this action.
        /// </remarks>
        /// <param name="roleId">The <see cref="Entity.Id"/> of the role.</param>
        /// <param name="permisions">The list of <see cref="ClubPermissionType"/> to remove.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        public void RemovePermissionsToRole(Guid roleId, IEnumerable<ClubPermissionType> permisions, Guid actorId)
        {
            if (permisions is null || permisions.Count() == 0)
                throw new ArgumentException("The permission list cannot be null or empty.", nameof(permisions));

            foreach (var permission in permisions)
                RemovePermissionToRole(roleId, permission, actorId);
        }

        /// <summary>
        /// Removes a <see cref="ClubRole"/> from a <see cref="ClubMember"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemoveRoleToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberId">The <see cref="Entity.Id"/> of the member.</param>
        /// <param name="roleId">The <see cref="Entity.Id"/> of the role.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="clubMemberId"/> is not in the club, or when the <paramref name="clubMemberId"/> doesn't have the <paramref name="roleId"/>.
        /// </exception>
        public void RemoveRoleToMember(Guid clubMemberId, Guid roleId, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.RemoveRoleToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == clubMemberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(clubMemberId));

            var role = _roles.FirstOrDefault(r => r.Id == roleId);
            // Throw an error if the member is the owner and the role is the owner role
            if (role is not null && role.IsOwnerRole && member == Owner)
                throw new UnauthorizedAccessException("The owner of the club can't be removed from the owner role");

            // Throw an error if following conditions are met:
            // - The role is the owner role
            // - There is only one user left in the club
            if ((role is not null && role.IsOwnerRole &&
                _members.Count(m => m.Roles.Contains(role)) <= 1))
                throw new UnauthorizedAccessException("At least one member must have the Owner role.");

                // Remove role to member
                member.RemoveRole(roleId);
        }

        /// <summary>
        /// Removes a list of <see cref="ClubRole"/> from a <see cref="ClubMember"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.RemoveRoleToMember"/> permission can perform this action.
        /// </remarks>
        /// <param name="clubMemberId">The <see cref="Entity.Id"/> of the member.</param>
        /// <param name="roleIds">The list of <see cref="Entity.Id"/> to remove.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="clubMemberId"/> is not in the club, or when the <paramref name="clubMemberId"/> doesn't have the <paramref name="roleId"/>.
        /// </exception>
        public void RemoveRolesToMember(Guid clubMemberId, IEnumerable<Guid> roleIds, Guid actorId)
        {
            if (roleIds is null || roleIds.Count() == 0)
                throw new ArgumentException("The role ID list cannot be null or empty.", nameof(roleIds));

            foreach (var roleId in roleIds)
                RemoveRoleToMember(clubMemberId, roleId, actorId);
        }

        /// <summary>
        /// Updates the <see cref="Address"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.ChangeAddress"/> permission can perform this action.
        /// </remarks>
        /// <param name="newAddress">The new <see cref="Address"/> of the club.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newAddress"/> is <see langword="null"/> or <see langword="empty"/>.
        /// </exception>
        public void UpdateAddress(Address? newAddress, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.ChangeAddress);
            ArgumentNullException.ThrowIfNull(newAddress, nameof(newAddress));
            _address = newAddress;

        }

        /// <summary>
        /// Updates the <see cref="Description"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.ChangeDescription"/> permission can perform this action.
        /// </remarks>
        /// <param name="newDescription">The new <see cref="Description"/> of the club.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newDescription"/> is <see langword="null"/> or <see langword="empty"/>.
        /// </exception>
        public void UpdateDescription(string? newDescription, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.ChangeDescription);
            ArgumentException.ThrowIfNullOrWhiteSpace(newDescription, nameof(newDescription));
            _description = newDescription;
        }

        /// <summary>
        /// Updates the <see cref="Name"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.ChangeName"/> permission can perform this action.
        /// </remarks>
        /// <param name="newName">The new <see cref="Name"/> of the club.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newName"/> is <see langword="null"/> or <see langword="empty"/>.
        /// </exception>
        public void UpdateName(string? newName, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.ChangeName);
            ArgumentException.ThrowIfNullOrWhiteSpace(newName, nameof(newName));
            _name = newName;
        }

        /// <summary>
        /// Updates the <see cref="Owner"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only the <paramref name="userId"/> who is the <see cref="Owner"/> of the <see cref="Club"/> can perform this action.
        /// </remarks>
        /// <param name="newOwnerId">The <see cref="Entity.Id"/> of the new <see cref="ClubMember"/> owner.</param>
        /// <param name="ownerId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="ownerId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="newOwnerId"/> is not in the club.
        /// </exception>
        public void UpdateOwner(Guid newOwnerId, Guid ownerId)
        {
            if (!this.IsOwner(ownerId))
                throw new UnauthorizedAccessException("Only the Owner of the club can change the ownership.");

            // Get the member of the club that will be the new owner
            var member = _members.FirstOrDefault(m => m.Id == newOwnerId || m.UserId == newOwnerId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(newOwnerId));

            // Replace owner by the member
            Owner = member;
        }

        /// <summary>
        /// Updates the <see cref="ClubRole.Name"/> of a <see cref="ClubRole"/> within the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.ChangeRoleName"/> permission can perform this action.
        /// </remarks>
        /// <param name="newName">The new name of the role.</param>
        /// <param name="roleId">The <see cref="ClubRole.Id"/> of the role.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="roleId"/> is not in the club.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="newName"/> is <see langword="null"/> or <see langword="empty"/>
        /// </exception>
        public void UpdateRoleName(string newName, Guid roleId, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.ChangeRoleName);

            // Throw an exception if the member does not exist in the club
            var role = _roles.FirstOrDefault(m => m.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));
            role.UpdateName(newName);
        }

        /// <summary>
        /// Updates the <see cref="Visibility"/> of the <see cref="Club"/>.
        /// </summary>
        /// <remarks>
        /// Only <paramref name="actorId"/> with the <see cref="ClubPermissionType.ChangeVisibility"/> permission can perform this action.
        /// </remarks>
        /// <param name="visibility">The new visibility of the club.</param>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        public void UpdateVisibility(ClubVisibility visibility, Guid actorId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateMemberPermission(actorId, ClubPermissionType.ChangeVisibility);
            Visibility = visibility;
        }

        /// <summary>
        /// Validates if a user is authorized to perform an action.
        /// </summary>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <param name="permission">The required <see cref="ClubPermissionType"/> to perform the action</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the <paramref name="actorId"/> is not authorized to perform the action.
        /// </exception>
        private void ValidateMemberPermission(Guid actorId, ClubPermissionType permission)
        {
            if (!IsMemberAuthorized(actorId, permission))
                throw new UnauthorizedAccessException($"Club member does not have the permission to {permission}");

        }

        /// <summary>
        /// Verifies if a member is authorized to perform an action.
        /// </summary>
        /// <remarks>
        /// The user is authorized if any of the following conditions are met:
        /// <list type="bullet">
        /// <item>The user has the permission as a <see cref="ClubMember"/>.</item>
        /// <item>The user has the permission through a <see cref="ClubRole"/>.</item>
        /// <item>The user is the <see cref="Owner"/> of the <see cref="Club"/>.</item>
        /// </list>
        /// </remarks>
        /// <param name="actorId">The <see cref="Entity.Id"/> or the <see cref="ClubMember.UserId"/> of the club member performing the action.</param>
        /// <param name="permission">The required <see cref="ClubPermissionType"/> to perform the action.</param>
        /// <returns>
        /// <see langword="true"/> if the user has the required permission, otherwise <see langword="false"/>.
        /// </returns>
        private bool IsMemberAuthorized(Guid actorId, ClubPermissionType permission)
        {
            // Get the member based on the specified user ID
            var member = _members.FirstOrDefault(m => m.Id == actorId || m.UserId == actorId);

            // Return false if the user is not part of the club
            if (member is null)
                return false;

            // Return true if the user has permission to perform the action.
            // A user can have permission if:
            // - They have the permission as a member
            // - They have permission through a role
            // - He's the owner of club
            return (member.HasPermission(permission) ||
                    this.IsOwner(actorId));
                
        }

        /// <summary>
        /// Verifies that the specified id corresponds to the <see cref="Owner"/> of the club.
        /// </summary>
        /// <param name="actorId">The unique identifier to verify</param>
        /// <returns><see langword="true"/> if the <paramref name="actorId"/> is the <see cref="Owner"/>, otherwise <see langword="false"/></returns>
        private bool IsOwner(Guid actorId)
        {
            return Owner.Id == actorId || Owner.UserId == actorId;
        }
    }
}
