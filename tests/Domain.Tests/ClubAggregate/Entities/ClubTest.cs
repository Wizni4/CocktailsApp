/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.Shared;

/*
 * Framework namespaces
 */


namespace CocktailsApp.Domain.Tests.ClubAggregate
{
    public class ClubTest
    {
        private readonly Address _address = new("123 Main St", "456", "Springfield", "12345", "IL", "USA");
        private readonly string _description = "A club for testing.";
        private readonly string _name = "Test Club";
        private readonly Guid _ownerUserId = Guid.NewGuid();
        private readonly Guid _cocktailId = Guid.NewGuid();
        private readonly Guid _memberUserId = Guid.NewGuid();

        private Club ReturnClub() => new ClubBuilder()
            .WithAddress(_address)
            .WithDescription(_description)
            .WithName(_name)
            .WithOwner(_ownerUserId)
            .WithVisibility(ClubVisibility.Public)
            .Build();

        #region AddCocktail

        [Test]
        public void AddCocktail_Owner_CocktailAdded()
        {
            // Arrange
            var club = ReturnClub();

            // Act
            club.AddCocktail(_cocktailId, _ownerUserId);

            // Assert
            var newCocktail = club.Cocktails.First(m => m.CocktailId == _cocktailId);
            Assert.Multiple(() =>
            {
                Assert.That(club.Cocktails, Has.Count.EqualTo(1));
                Assert.That(newCocktail.CocktailId, Is.EqualTo(_cocktailId));
                Assert.That(newCocktail.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(newCocktail.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                Assert.That(newCocktail.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test]
        public void AddCocktail_PermittedMember_CocktailAdded()
        {
            // Arrange
            var club = ReturnClub();
            club.AddMember(_memberUserId, _ownerUserId);
            var newMember = club.Members.First(m => m.UserId == _memberUserId);
            club.AddPermissionToMember(newMember.Id, ClubPermission.AddCocktail, _ownerUserId);

            // Act
            club.AddCocktail(_cocktailId, _memberUserId);

            // Assert
            var newCocktail = club.Cocktails.First(m => m.CocktailId == _cocktailId);
            Assert.Multiple(() =>
            {
                Assert.That(club.Cocktails, Has.Count.EqualTo(1));
                Assert.That(newCocktail.CocktailId, Is.EqualTo(_cocktailId));
                Assert.That(newCocktail.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(newCocktail.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                Assert.That(newCocktail.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test]
        public void AddCocktail_PermittedRole_CocktailAdded()
        {
            // Arrange
            var club = ReturnClub();

            // Add new member
            var memberId = Guid.NewGuid();
            club.AddMember(memberId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == memberId);

            // Create new role & add permission to the role
            var roleName = "Test role";
            club.CreateRole(roleName, _ownerUserId);
            var role = club.Roles.First(r => r.Name == roleName);
            club.AddRolePermission(role.Id, ClubPermission.AddCocktail, _ownerUserId);

            // Add Role to member
            club.AddRoleToMember(member.Id, role.Id, _ownerUserId);



            // Act
            club.AddCocktail(_cocktailId, memberId);

            // Assert
            var newCocktail = club.Cocktails.First(m => m.CocktailId == _cocktailId);
            Assert.Multiple(() =>
            {
                Assert.That(club.Cocktails, Has.Count.EqualTo(1));
                Assert.That(newCocktail.CocktailId, Is.EqualTo(_cocktailId));
                Assert.That(newCocktail.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(newCocktail.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                Assert.That(newCocktail.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test]
        public void AddCocktail_NotPermittedMember_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();
            var newMemberId = Guid.NewGuid();
            club.AddMember(newMemberId, _ownerUserId);
            var newMember = club.Members.First(m => m.UserId == newMemberId);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddCocktail(_cocktailId, newMember.Id));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddCocktail"));
        }

        [Test]
        public void AddCocktail_NotAMember_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddCocktail(_cocktailId, Guid.NewGuid()));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddCocktail"));
        }

        [Test]
        public void AddCocktail_CocktailAlreadyInClub_ThrowsArgumentException()
        {
            // Arrange
            var club = ReturnClub();
            club.AddCocktail(_cocktailId, _ownerUserId);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                club.AddCocktail(_cocktailId, _ownerUserId));

            Assert.That(exception.Message, Is.EqualTo("This cocktail is already in the club. (Parameter 'cocktailId')"));
        }

        [Test]
        public void AddCocktail_EmptyCocktailId_ThrowsArgumentNullException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                club.AddCocktail(Guid.Empty, _ownerUserId));

            Assert.That(exception.Message, Is.EqualTo("CocktailId cannot be null. (Parameter 'cocktailId')"));
        }

        [Test]
        public void AddCocktail_EmptyUserId_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddCocktail(_cocktailId, Guid.Empty));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddCocktail"));
        }

        #endregion

        #region AddMember

        [Test]
        public void AddMember_Owner_MemberAdded()
        {
            // Arrange
            var club = ReturnClub();

            // Act
            club.AddMember(_memberUserId, _ownerUserId);

            // Assert
            var newMember = club.Members.First(m => m.UserId == _memberUserId);
            Assert.Multiple(() =>
            {
                Assert.That(club.Members, Has.Count.EqualTo(2));
                Assert.That(newMember.UserId, Is.EqualTo(_memberUserId));
                Assert.That(newMember.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(newMember.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                Assert.That(newMember.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test]
        public void AddMember_PermittedMember_MemberAdded()
        {
            // Arrange
            var club = ReturnClub();
            var memberId = Guid.NewGuid();
            club.AddMember(memberId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == memberId);
            club.AddPermissionToMember(member.Id, ClubPermission.AddMember, _ownerUserId);

            // Act
            club.AddMember(_memberUserId, memberId);

            // Assert
            var newMember = club.Members.First(m => m.UserId == _memberUserId);
            Assert.Multiple(() =>
            {
                Assert.That(club.Members, Has.Count.EqualTo(3));
                Assert.That(newMember.UserId, Is.EqualTo(_memberUserId));
                Assert.That(newMember.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(newMember.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                Assert.That(newMember.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test]
        public void AddMember_PermittedRole_MemberAdded()
        {
            // Arrange
            var club = ReturnClub();

            // Add new member
            var memberId = Guid.NewGuid();
            club.AddMember(memberId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == memberId);

            // Create new role & add permission to the role
            var roleName = "Test role";
            club.CreateRole(roleName, _ownerUserId);
            var role = club.Roles.First(r => r.Name == roleName);
            club.AddRolePermission(role.Id, ClubPermission.AddMember, _ownerUserId);

            // Add Role to member
            club.AddRoleToMember(member.Id, role.Id, _ownerUserId);




            // Act
            club.AddMember(_memberUserId, memberId);

            // Assert
            var newMember = club.Members.First(m => m.UserId == _memberUserId);
            Assert.Multiple(() =>
            {
                Assert.That(club.Members, Has.Count.EqualTo(3));
                Assert.That(newMember.UserId, Is.EqualTo(_memberUserId));
                Assert.That(newMember.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(newMember.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                Assert.That(newMember.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test]
        public void AddMember_NotPermittedMember_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();
            var memberId = Guid.NewGuid();
            club.AddMember(memberId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == memberId);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddMember(_memberUserId, member.Id));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddMember"));
        }

        [Test]
        public void AddMember_NotAMember_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddMember(_memberUserId, Guid.NewGuid()));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddMember"));
        }

        [Test]
        public void AddMember_MemberAlreadyInClub_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();
            club.AddMember(_memberUserId, _ownerUserId);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                club.AddMember(_memberUserId, _ownerUserId));

            Assert.That(exception.Message, Is.EqualTo("This member is already in the club. (Parameter 'memberId')"));
        }

        [Test]
        public void AddMember_EmptyMemberId_ThrowsArgumentNullException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                club.AddMember(Guid.Empty, _ownerUserId));

            Assert.That(exception.Message, Is.EqualTo("UserId cannot be null. (Parameter 'userId')"));
        }

        [Test]
        public void AddMember_EmptyUserId_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddMember(_memberUserId, Guid.Empty));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddMember"));
        }

        #endregion

        #region AddPermissionToMember

        [Test]
        public void AddPermissionToMember_Owner_PermissionAdded()
        {
            // Arrange
            var club = ReturnClub();
            club.AddMember(_memberUserId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == _memberUserId);
            var permission = ClubPermission.RemoveRoleToMember;

            // Act
            club.AddPermissionToMember(member.Id, permission, _ownerUserId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(member.Permissions, Has.Count.EqualTo(1));
                Assert.That(member.Permissions.Any(p => p == permission), Is.True);
            });
        }

        [Test]
        public void AddPermissionToMember_PermittedMember_PermissionAdded()
        {
            // Arrange
            var club = ReturnClub();
            club.AddMember(_memberUserId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == _memberUserId);
            club.AddPermissionToMember(member.Id, ClubPermission.AddPermissionToMember, _ownerUserId);
            var permission = ClubPermission.RemoveRoleToMember;

            // Act
            club.AddPermissionToMember(member.Id, permission, _memberUserId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(member.Permissions, Has.Count.EqualTo(2));
                Assert.That(member.Permissions.Any(p => p == permission), Is.True);
            });
        }

        [Test]
        public void AddPermissionToMember_PermittedRole_PermissionAdded()
        {
            // Arrange
            var club = ReturnClub();

            // Add new member
            club.AddMember(_memberUserId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == _memberUserId);

            // Create new role & add permission to the role
            var roleName = "Test role";
            club.CreateRole(roleName, _ownerUserId);
            var role = club.Roles.First(r => r.Name == roleName);
            club.AddRolePermission(role.Id, ClubPermission.AddPermissionToMember, _ownerUserId);

            // Add Role to member
            club.AddRoleToMember(member.Id, role.Id, _ownerUserId);

            // Define permission to add to the member
            var permission = ClubPermission.RemoveRoleToMember;



            // Act
            club.AddPermissionToMember(member.Id, permission, _memberUserId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(member.Permissions, Has.Count.EqualTo(1));
                Assert.That(member.Permissions.Any(p => p == permission), Is.True);
            });
        }

        [Test]
        public void AddPermissionToMember_NotPermittedMember_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();
            club.AddMember(_memberUserId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == _memberUserId);
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddPermissionToMember(member.Id, permission, _memberUserId));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddPermissionToMember"));
        }

        [Test]
        public void AddPermissionToMember_NotAMember_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();
            club.AddMember(_memberUserId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == _memberUserId);
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddPermissionToMember(member.Id, permission, Guid.NewGuid()));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddPermissionToMember"));
        }

        [Test]
        public void AddPermissionToMember_MemberNotInClub_ThrowsArgumentException()
        {
            // Arrange
            var club = ReturnClub();
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                club.AddPermissionToMember(Guid.NewGuid(), permission, _ownerUserId));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'memberId')"));
        }

        [Test]
        public void AddPermissionToMember_MemberAlreadyHavePermission_ThrowsArgumentException()
        {
            // Arrange
            var club = ReturnClub();
            club.AddMember(_memberUserId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == _memberUserId);
            var permission = ClubPermission.RemoveRoleToMember;
            club.AddPermissionToMember(member.Id, permission, _ownerUserId);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                club.AddPermissionToMember(member.Id, permission, _ownerUserId));

            Assert.That(exception.Message, Is.EqualTo("The member already has specified permission. (Parameter 'permission')"));
        }

        #endregion

        #region AddRolePermission

        [Test]
        public void AddRolePermission_Owner_PermissionAdded()
        {
            // Arrange
            var club = ReturnClub();
            var roleName = "Test role";
            club.CreateRole(roleName, _ownerUserId);
            var role = club.Roles.First(r => r.Name == roleName);
            var permission = ClubPermission.RemoveRoleToMember;

            // Act
            club.AddRolePermission(role.Id, permission, _ownerUserId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(role.Permissions, Has.Count.EqualTo(1));
                Assert.That(role.Permissions.Any(p => p == permission), Is.True);
            });
        }

        [Test]
        public void AddRolePermission_PermittedMember_PermissionAdded()
        {
            // Arrange
            var club = ReturnClub();
            var permission = ClubPermission.RemoveRoleToMember;

            // Add new member with permission
            club.AddMember(_memberUserId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == _memberUserId);
            club.AddPermissionToMember(member.Id, ClubPermission.AddRolePermission, _ownerUserId);

            // Create new role
            var roleName = "Test role";
            club.CreateRole(roleName, _ownerUserId);
            var role = club.Roles.First(r => r.Name == roleName);

            // Act
            club.AddRolePermission(role.Id, permission, _memberUserId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(role.Permissions, Has.Count.EqualTo(1));
                Assert.That(role.Permissions.Any(p => p == permission), Is.True);
            });
        }

        [Test]
        public void AddRolePermission_PermittedRole_PermissionAdded()
        {
            // Arrange
            var club = ReturnClub();
            var permission = ClubPermission.RemoveRoleToMember;

            // Create new role with permission
            var roleName = "Test role";
            club.CreateRole(roleName, _ownerUserId);
            var role = club.Roles.First(r => r.Name == roleName);
            club.AddRolePermission(role.Id, ClubPermission.AddRolePermission, _ownerUserId);

            // Add new member with role
            club.AddMember(_memberUserId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == _memberUserId);
            club.AddRoleToMember(member.Id, role.Id, _ownerUserId);

            // Act
            club.AddRolePermission(role.Id, permission, _memberUserId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(role.Permissions, Has.Count.EqualTo(2));
                Assert.That(role.Permissions.Any(p => p == permission), Is.True);
            });
        }

        [Test]
        public void AddRolePermission_NotPermittedMember_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();
            var permission = ClubPermission.RemoveRoleToMember;

            // Create new role without permissions
            var roleName = "Test role";
            club.CreateRole(roleName, _ownerUserId);
            var role = club.Roles.First(r => r.Name == roleName);

            // Add new member without permissions
            club.AddMember(_memberUserId, _ownerUserId);
            var member = club.Members.First(m => m.UserId == _memberUserId);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddRolePermission(role.Id, permission, _memberUserId));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddRolePermission"));
        }

        [Test]
        public void AddRolePermission_NotAMember_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();
            var permission = ClubPermission.RemoveRoleToMember;

            // Create new role without permissions
            var roleName = "Test role";
            club.CreateRole(roleName, _ownerUserId);
            var role = club.Roles.First(r => r.Name == roleName);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddRolePermission(role.Id, permission, Guid.NewGuid()));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddRolePermission"));
        }

        [Test]
        public void AddRolePermission_RoleNotInClub_ThrowsArgumentException()
        {
            // Arrange
            var club = ReturnClub();
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                club.AddRolePermission(Guid.NewGuid(), permission, _ownerUserId));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void AddRolePermission_RoleAlreadyHasPermission_ThrowsArgumentException()
        {
            // Arrange
            var club = ReturnClub();
            var permission = ClubPermission.RemoveRoleToMember;

            // Create new role with permission
            var roleName = "Test role";
            club.CreateRole(roleName, _ownerUserId);
            var role = club.Roles.First(r => r.Name == roleName);
            club.AddRolePermission(role.Id, permission, _ownerUserId);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                club.AddRolePermission(role.Id, permission, _ownerUserId));

            Assert.That(exception.Message, Is.EqualTo("The role already has the permission. (Parameter 'permission')"));
        }

        #endregion
    }
}
