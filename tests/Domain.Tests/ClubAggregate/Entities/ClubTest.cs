/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.Shared;

using NUnit.Framework.Internal;

using System;
using System.Data;
using System.Security;

/*
 * Framework namespaces
 */


namespace CocktailsApp.Domain.Tests.ClubAggregate
{
    [TestFixture]
    public class ClubTest
    {
        private Club _club;
        private ClubMember _member;
        private ClubRole _role;
        private Guid _userId;
        private Guid _ownerId;

        [SetUp]
        public void SetUp()
        {
            _userId = Guid.NewGuid();
            _ownerId = Guid.NewGuid();
            _club = new ClubBuilder()
                .WithAddress(new("123 Main St", "456", "Springfield", "12345", "IL", "USA"))
                .WithDescription("A club for testing.")
                .WithName("Test Club")
                .WithOwner(_ownerId)
                .WithVisibility(ClubVisibility.Public)
                .Build();

            // Create Test role
            var roleName = "Test Role";
            _club.CreateRole(roleName, _ownerId);
            _role = _club.Roles.First(r => r.Name == roleName);

            // Create Test Member
            _club.AddMember(_userId, _ownerId);
            _member = _club.Members.First(m => m.UserId == _userId);
            _member.AddRole(_role);
        }

        #region Permissions

        private static IEnumerable<TestCaseData> PermissionTestCases
        {
            get
            {
                return
                [
                //  new TestCaseData(scenario       , shouldSucceed),
                    new TestCaseData("owner"        , true         ),
                    new TestCaseData("member"       , true         ),
                    new TestCaseData("role"         , true         ),
                    new TestCaseData("not_permitted", false        ),
                    new TestCaseData("not_a_member" , false        ),
                    new TestCaseData("empty_user_id", false        ),
                ];
            }
        }

        private void RunPermissionScenarios(
            ClubPermission permission,
            string scenario,
            bool shouldSucceed,
            Action<Guid> action,
            Action assert)
        {
            // Arrange
            var userId = _userId;
            switch (scenario)
            {
                case "owner":
                    _club.UpdateOwner(_member.Id, _ownerId);
                    break;
                case "member":
                    _member.AddPermission(permission);
                    break;
                case "role":
                    _role.AddPermission(permission);
                    break;
                case "not_a_member":
                    _club.RemoveMember(_member.Id, _ownerId);
                    break;
                case "empty_user_id":
                    userId = Guid.Empty;
                    break;
                default:
                    break;
            }

            // Act & Assert
            if (shouldSucceed)
            {
                // Act
                action(userId);

                // Assert
                assert();
            }
            else
            {
                var exception = Assert.Throws<UnauthorizedAccessException>(() => action(userId));
                Assert.That(exception.Message, Is.EqualTo($"User does not have permission to {permission}"));
            }
        }

        #endregion

        #region AddCocktail

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddCocktail_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.AddCocktail;
            var cocktailId = Guid.NewGuid();
            var action = (Guid userId) => _club.AddCocktail(cocktailId, userId);
            var assert = () =>
            {
                // Assert
                var newCocktail = _club.Cocktails.First(m => m.CocktailId == cocktailId);
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Cocktails, Has.Count.EqualTo(1));
                    Assert.That(newCocktail.CocktailId, Is.EqualTo(cocktailId));
                    Assert.That(newCocktail.Id, Is.Not.EqualTo(Guid.Empty));
                    Assert.That(newCocktail.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                    Assert.That(newCocktail.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                });
            };
            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                assert);
        }

        [Test]
        public void AddCocktail_EmptyCocktailId_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _club.AddCocktail(Guid.Empty, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("CocktailId cannot be null. (Parameter 'cocktailId')"));
        }

        [Test]
        public void AddCocktail_CocktailAlreadyInClub_ThrowsArgumentException()
        {
            //Arrange
            var cocktailId = Guid.NewGuid();
            _club.AddCocktail(cocktailId, _ownerId);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddCocktail(cocktailId, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("This cocktail is already in the club. (Parameter 'cocktailId')"));
        }

        #endregion

        #region AddMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.AddMember;
            var memberId = Guid.NewGuid();
            var action = (Guid userId) => _club.AddMember(memberId, userId);
            var assert = () =>
            {
                // Assert
                var newMember = _club.Members.First(m => m.UserId == memberId);
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Members, Has.Count.EqualTo(3));
                    Assert.That(newMember.UserId, Is.EqualTo(memberId));
                    Assert.That(newMember.Id, Is.Not.EqualTo(Guid.Empty));
                    Assert.That(newMember.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                    Assert.That(newMember.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                });
            };
            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                assert);
        }

        [Test]
        public void AddMember_EmptyMemberId_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _club.AddMember(Guid.Empty, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("UserId cannot be null. (Parameter 'userId')"));
        }

        [Test]
        public void AddMember_MemberAlreadyInClub_ThrowsUnauthorizedAccessException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddMember(_member.UserId, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("This member is already in the club. (Parameter 'memberId')"));
        }

        #endregion

        #region AddPermissionToMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddPermissionToMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.AddPermissionToMember;
            var permissionAdded = ClubPermission.RemoveRoleToMember;
            var action = (Guid userId) => _club.AddPermissionToMember(_member.Id, permissionAdded, userId);
            var assert = () => Assert.That(_member.Permissions.Any(p => p == permissionAdded), Is.True);

            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                assert);
        }

        [Test]
        public void AddPermissionToMember_MemberNotInClub_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddPermissionToMember(Guid.NewGuid(), permission, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'memberId')"));
        }

        [Test]
        public void AddPermissionToMember_EmptyMemberId_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddPermissionToMember(Guid.Empty, permission, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'memberId')"));
        }

        [Test]
        public void AddPermissionToMember_MemberAlreadyHasPermission_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;
            _member.AddPermission(permission);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddPermissionToMember(_member.Id, permission, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The member already has specified permission. (Parameter 'permission')"));
        }

        #endregion

        #region AddRolePermission

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddRolePermission_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.AddRolePermission;
            var permissionAdded = ClubPermission.RemoveRoleToMember;
            var action = (Guid userId) => _club.AddRolePermission(_role.Id, permissionAdded, userId);
            var assert = () => Assert.That(_role.Permissions.Any(p => p == permissionAdded), Is.True);

            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                assert);
        }

        [Test]
        public void AddRolePermission_RoleNotInClub_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRolePermission(Guid.NewGuid(), permission, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void AddRolePermission_EmptyRoleId_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRolePermission(Guid.Empty, permission, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void AddRolePermission_RoleAlreadyHasPermission_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;
            _role.AddPermission(permission);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRolePermission(_role.Id, permission, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The role already has the permission. (Parameter 'permission')"));
        }

        #endregion

        #region AddRoleToMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddRoleToMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {    
            // Create role
            var roleName = "Test role";
            _club.CreateRole(roleName, _ownerId);
            var role = _club.Roles.First(r => r.Name == roleName);

            // Arrange
            var permission = ClubPermission.AddRoleToMember;
            var action = (Guid userId) => _club.AddRoleToMember(_member.Id, role.Id, userId);
            var assert = () => Assert.That(_member.Roles.Any(r => r.Name == roleName), Is.True);

            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                assert);
        }

        [Test]
        public void AddRoleToMember_MemberNotInClub_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRoleToMember(Guid.NewGuid(), _role.Id, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'memberId')"));
        }

        [Test]
        public void AddRoleToMember_RoleNotInClub_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRoleToMember(_member.Id, Guid.NewGuid(), _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void AddRoleToMember_MemberAlreadyHasRole_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRoleToMember(_member.Id, _role.Id, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The member already has this role. (Parameter 'role')"));
        }

        #endregion

        #region CreateRole

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void CreateRole_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.CreateRole;
            var roleName = "Test role";
            var action = (Guid userId) => _club.CreateRole(roleName, userId);
            var assert = () =>
            {
                var role = _club.Roles.First(r => r.Name == roleName);
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Roles, Has.Count.EqualTo(2));
                    Assert.That(role.Name, Is.EqualTo(roleName));
                    Assert.That(role.Id, Is.Not.EqualTo(Guid.Empty));
                    Assert.That(role.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                    Assert.That(role.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                });
            };

            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                assert);
        }

        [Test]
        public void CreateRole_EmptyName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.CreateRole("", _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'newName')"));
        }

        [Test]
        public void CreateRole_NullName_ThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _club.CreateRole(null, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'newName')"));
        }

        [Test]
        public void CreateRole_WitheSpaceName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.CreateRole("  ", _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'newName')"));
        }

        [Test]
        public void CreateRole_RoleAlreadyInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.CreateRole(_role.Name, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("This role name already exists. (Parameter 'roleName')"));
        }

        #endregion

        #region DeleteRole

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void DeleteRole_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Create role
            var roleName = "Test role";
            _club.CreateRole(roleName, _ownerId);
            var role = _club.Roles.First(r => r.Name == roleName);
            _member.AddRole(role);

            // Arrange
            var permission = ClubPermission.DeleteRole;
            var action = (Guid userId) => _club.DeleteRole(role.Id, userId);
            var assert = () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Roles, Has.Count.EqualTo(1));
                    Assert.That(_club.Roles.Any(r => r.Id == role.Id), Is.False);
                    Assert.That(_member.Roles.Any(r => r.Id == role.Id), Is.False);
                });
            };

            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                assert);
        }

        [Test]
        public void DeleteRole_RoleNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.DeleteRole(Guid.NewGuid(), _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void DeleteRole_EmptyRoleId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.DeleteRole(Guid.Empty, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        #endregion
    }
}
