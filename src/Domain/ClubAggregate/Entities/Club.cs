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
        public IReadOnlyCollection<ClubCocktail> Cocktails { get { return _cocktails.AsReadOnly(); } }
        public string Description { get; private set; }
        private readonly List<ClubRole> _roles = [];
        public IReadOnlyCollection<ClubRole> Roles { get { return _roles.AsReadOnly(); } }
        private readonly List<ClubMember> _members = [];
        public IReadOnlyCollection<ClubMember> Members { get { return _members.AsReadOnly(); } }
        public string Name { get; private set; }
        public ClubVisibility Visibility { get; private set; }

        internal Club(string description, string name, ClubVisibility visibility)
        {
            Description = description;
            Name = name;
            Visibility = visibility;
        }

        internal Club(Address address, string description, string name, ClubVisibility visibility)
        {
            Address = address;
            Description = description;
            Name = name;
            Visibility = visibility;
        }

        public void AddCocktail(Guid cocktailId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.AddCocktail);

            // To avoid duplicates, make sure the cocktail is not already in the club
            if (_cocktails.Any(cc => cc.CocktailId == cocktailId))
                throw new ArgumentException("This role name already exists", nameof(cocktailId));

            _cocktails.Add(new ClubCocktail(cocktailId));
        }

        public void AddMember(Guid memberId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.AddMember);

            // Verify that the specified user is not already ine the club. If yes throw an argument exception.
            if (_members.Any(m => m.UserId == memberId))
                throw new ArgumentException("The specified member is already in the club.", nameof(memberId));

            // Create and add a new member to the club.
            _members.Add(new ClubMember(memberId));
        }

        public void AddRolePermission(Guid roleId, ClubAction action, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.AddRolePermission);

            // Throw an exception if specified role does not exist in the club
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("This role could not be found in the club.", nameof(roleId));

            // Add the permission to the role
            role.AddPermission(action);
        }

        public void AddRolePermissions(Guid roleId, IEnumerable<ClubAction> actions, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.AddRolePermission);

            // Throw an exception if specified role does not exist in the club
            var role = _roles.FirstOrDefault(r => r.Id == roleId) ?? throw new ArgumentException("This role could not be found in the club.", nameof(roleId));

            // Add the permission to the role
            role.AddPermissions(actions);
        }

        public void AddPermissionToMember(Guid memberId, ClubAction action, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.AddPermissionToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));

            // Add permission to the member
            // Throw an exception if the member already have this permission.
            member.AddPermission(action);
        }

        public void AddPermissionsToMember(Guid memberId, IEnumerable<ClubAction> actions, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.AddPermissionToMember);

            // Throw an exception if the member does not exist in the club
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));

            // Add permissions to the member
            // Throw an exception if the member already have these permissions.
            member.AddPermissions(actions);
        }

        public void AddRoleToMember(Guid memberId, Guid roleId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.AddRoleToMember);

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
            ValidateUserPermission(userId, ClubAction.CreateRole);

            // Ensure the role name is unique within the club to avoid duplicates.
            if (_roles.Any(g => g.Name == roleName))
                throw new ArgumentException("This role name already exists", nameof(roleName));

            _roles.Add(new ClubRole(roleName));
        }

        public void RemoveCocktail(Guid cocktailId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.RemoveCocktail);

            // Throw an error if the club doesn have the cocktail.
            var cocktail = _cocktails.FirstOrDefault(c => c.Id == cocktailId) ?? throw new ArgumentException("The cocktail could not be found in the club.", nameof(cocktailId));
            _cocktails.Remove(cocktail);
        }

        public void RemoveMember(Guid memberId, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.RemoveMember);

            // Throw an error if the member is not in the club.
            var member = _members.FirstOrDefault(m => m.Id == memberId) ?? throw new ArgumentException("The member could not be found in the club.", nameof(memberId));
            _members.Remove(member);
        }

        public void UpdateAddress(Address newAddress, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.ChangeAddress);
            Address = newAddress;

        }

        public void UpdateDescription(string newDescription, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.ChangeDescription);
            Description = newDescription;
        }

        public void UpdateName(string newName, Guid userId)
        {
            // Check the user's permissions:
            // Raise an exception if the user doesn't have permission to perform the action.
            ValidateUserPermission(userId, ClubAction.ChangeName);
            Name = newName;
        }

        private void ValidateUserPermission(Guid userId, ClubAction action)
        {
            if (!IsMemberAuthorized(userId, action))
                throw new UnauthorizedAccessException($"User does not have permission to {action}");

        }

        private bool IsMemberAuthorized(Guid userId, ClubAction action)
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
            return (member.HasPermission(action) ||
                    member.Roles.Any(r => r.HasPermission(action)));
                
        }
    }
}
