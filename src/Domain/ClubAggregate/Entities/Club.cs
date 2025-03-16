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
    public class Club : Entity, IAggregateRoot
    {
        public Address? Address { get; private set; }
        private readonly List<ClubCocktail> _cocktails = [];
        public IReadOnlyCollection<ClubCocktail> Cocktails { get => _cocktails.AsReadOnly(); }
        public string _description = null!;
        public string Description { get => _description; }
        private readonly List<ClubRole> _roles = [];
        public IReadOnlyCollection<ClubRole> Roles { get => _roles.AsReadOnly(); }
        private readonly List<ClubMember> _members = [];
        public IReadOnlyCollection<ClubMember> Members { get => _members.AsReadOnly(); }
        private string _name = null!;
        public string Name { get => _name; }
        public ClubMember Owner { get; private set; }
        public  ClubVisibility Visibility { get; private set; }

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

        public void AddRolePermission(Guid roleId, ClubPermission permission, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.AddRolePermission);

            // Throw an exception if specified role does not exist in the club
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("This role could not be found in the club.", nameof(roleId));

            // Add the permission to the role
            role.AddPermission(permission);
        }

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

        public void RemoveCocktail(Guid cocktailId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.RemoveCocktail);

            // Throw an error if the club doesn have the cocktail.
            var cocktail = _cocktails.FirstOrDefault(c => c.Id == cocktailId) ?? throw new ArgumentException("The cocktail could not be found in the club.", nameof(cocktailId));
            _cocktails.Remove(cocktail);
        }

        public void RemoveMember(Guid memberId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.RemoveMember);

            // Throw an error if the member is not in the club.
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));
            _members.Remove(member);
        }        

        public void RemovePermissionToMember(Guid memberId, ClubPermission action, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.RemovePermissionToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));

            // Remove permission to the member
            member.RemovePermission(action);
        }

        public void RemoveRolePermission(Guid roleId, ClubPermission action, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.RemoveRolePermission);

            // Throw an error if the role is not in the club.
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));
            role.RemovePermission(action);
        }

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

        public void UpdateAddress(Address? newAddress, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeAddress);
            ArgumentNullException.ThrowIfNull(newAddress, nameof(newAddress));
            Address = newAddress;

        }

        public void UpdateDescription(string? newDescription, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeDescription);
            ArgumentException.ThrowIfNullOrWhiteSpace(newDescription, nameof(newDescription));
            _description = newDescription;
        }

        public void UpdateName(string? newName, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeName);
            ArgumentException.ThrowIfNullOrWhiteSpace(newName, nameof(newName));
            _name = newName;
        }

        public void UpdateRoleName(string newName, Guid roleId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeRoleName);

            // Throw an exception if the member does not exist in the club
            var role = _roles.FirstOrDefault(m => m.Id == roleId) ?? throw new ArgumentException("The role could not be found in the club.", nameof(roleId));
            role.UpdateName(newName);
        }

        public void UpdateVisibility(ClubVisibility visibility, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubPermission.ChangeVisibility);
            Visibility = visibility;
        }

        private void ValidateUserPermission(Guid userId, ClubPermission permission)
        {
            if (!IsMemberAuthorized(userId, permission))
                throw new UnauthorizedAccessException($"User does not have permission to {permission}");

        }

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
