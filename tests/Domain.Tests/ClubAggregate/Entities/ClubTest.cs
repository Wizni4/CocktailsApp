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
        private ClubMember _owner;
        private ClubRole _role;
        private ClubCocktail _cocktail;

        [SetUp]
        public void SetUp()
        {
            _club = new ClubBuilder()
                .WithAddress(new("123 Main St", "456", "Springfield", "12345", "IL", "USA"))
                .WithDescription("A club for testing.")
                .WithName("Test Club")
                .WithOwner(Guid.NewGuid())
                .WithVisibility(ClubVisibility.Public)
                .Build();

            // Set the owner
            _owner = _club.Members.First();

            // Create Test role
            var roleName = "Test Role";
            _club.CreateRole(roleName, _owner.Id);
            _role = _club.Roles.First(r => r.Name == roleName);

            // Create Test Member
            var userId = Guid.NewGuid();
            _club.AddMember(userId, _owner.Id);
            _member = _club.Members.First(m => m.UserId == userId);
            _member.AddRole(_role);

            // Create Cocktail
            var cocktailId = Guid.NewGuid();
            _club.AddCocktail(cocktailId, _owner.Id);
            _cocktail = _club.Cocktails.First(c => c.CocktailId == cocktailId);
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
            var performingMemberId = _member.Id;
            switch (scenario)
            {
                case "owner":
                    _club.UpdateOwner(_member.Id, _owner.Id);
                    break;
                case "member":
                    _member.AddPermission(permission);
                    break;
                case "role":
                    _role.AddPermission(permission);
                    break;
                case "not_a_member":
                    _club.RemoveMember(_member.Id, _owner.Id);
                    break;
                case "empty_user_id":
                    performingMemberId = Guid.Empty;
                    break;
                default:
                    break;
            }

            // Act & Assert
            if (shouldSucceed)
            {
                // Act
                action(performingMemberId);

                // Assert
                assert();
            }
            else
            {
                var exception = Assert.Throws<UnauthorizedAccessException>(() => action(performingMemberId));
                Assert.That(exception.Message, Is.EqualTo($"Club member does not have the permission to {permission}"));
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
            var action = (Guid performingMemberId) => _club.AddCocktail(cocktailId, performingMemberId);
            var assert = () =>
            {
                // Assert
                var newCocktail = _club.Cocktails.First(m => m.CocktailId == cocktailId);
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Cocktails, Has.Count.EqualTo(2));
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
                _club.AddCocktail(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("CocktailId cannot be null. (Parameter 'cocktailId')"));
        }

        [Test]
        public void AddCocktail_CocktailAlreadyInClub_ThrowsArgumentException()
        {
            //Arrange
            var cocktailId = Guid.NewGuid();
            _club.AddCocktail(cocktailId, _owner.Id);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddCocktail(cocktailId, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("This cocktail is already in the club. (Parameter 'cocktailId')"));
        }

        #endregion

        #region AddMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.AddMember;
            var userId = Guid.NewGuid();
            var action = (Guid performingMemberId) => _club.AddMember(userId, performingMemberId);
            var assert = () =>
            {
                // Assert
                var newMember = _club.Members.First(m => m.UserId == userId);
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Members, Has.Count.EqualTo(3));
                    Assert.That(newMember.UserId, Is.EqualTo(userId));
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
                _club.AddMember(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("UserId cannot be null. (Parameter 'userId')"));
        }

        [Test]
        public void AddMember_MemberAlreadyInClub_ThrowsUnauthorizedAccessException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddMember(_member.UserId, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("This user is already a member of the club. (Parameter 'userId')"));
        }

        #endregion

        #region AddPermissionToMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddPermissionToMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.AddPermissionToMember;
            var permissionAdded = ClubPermission.RemoveRoleToMember;
            var action = (Guid performingMemberId) => _club.AddPermissionToMember(_member.Id, permissionAdded, performingMemberId);
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
                _club.AddPermissionToMember(Guid.NewGuid(), permission, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'clubMemberId')"));
        }

        [Test]
        public void AddPermissionToMember_EmptyMemberId_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddPermissionToMember(Guid.Empty, permission, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'clubMemberId')"));
        }

        [Test]
        public void AddPermissionToMember_MemberAlreadyHasPermission_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;
            _member.AddPermission(permission);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddPermissionToMember(_member.Id, permission, _owner.Id));

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
            var action = (Guid performingMemberId) => _club.AddRolePermission(_role.Id, permissionAdded, performingMemberId);
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
                _club.AddRolePermission(Guid.NewGuid(), permission, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void AddRolePermission_EmptyRoleId_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRolePermission(Guid.Empty, permission, _owner.Id));

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
                _club.AddRolePermission(_role.Id, permission, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role already has the permission. (Parameter 'permission')"));
        }

        #endregion

        #region AddRoleToMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddRoleToMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {    
            // Create role
            var roleName = "Test role";
            _club.CreateRole(roleName, _owner.Id);
            var role = _club.Roles.First(r => r.Name == roleName);

            // Arrange
            var permission = ClubPermission.AddRoleToMember;
            var action = (Guid performingMemberId) => _club.AddRoleToMember(_member.Id, role.Id, performingMemberId);
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
                _club.AddRoleToMember(Guid.NewGuid(), _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'clubMemberId')"));
        }

        [Test]
        public void AddRoleToMember_RoleNotInClub_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRoleToMember(_member.Id, Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void AddRoleToMember_MemberAlreadyHasRole_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRoleToMember(_member.Id, _role.Id, _owner.Id));

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
            var action = (Guid performingMemberId) => _club.CreateRole(roleName, performingMemberId);
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
                _club.CreateRole("", _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'newName')"));
        }

        [Test]
        public void CreateRole_NullName_ThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _club.CreateRole(null, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'newName')"));
        }

        [Test]
        public void CreateRole_WitheSpaceName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.CreateRole("  ", _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'newName')"));
        }

        [Test]
        public void CreateRole_RoleAlreadyInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.CreateRole(_role.Name, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("This role name already exists. (Parameter 'roleName')"));
        }

        #endregion

        #region DeleteRole

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void DeleteRole_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Create role
            var roleName = "Test role";
            _club.CreateRole(roleName, _owner.Id);
            var role = _club.Roles.First(r => r.Name == roleName);
            _member.AddRole(role);

            // Arrange
            var permission = ClubPermission.DeleteRole;
            var action = (Guid performingMemberId) => _club.DeleteRole(role.Id, performingMemberId);
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
                _club.DeleteRole(Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void DeleteRole_EmptyRoleId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.DeleteRole(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        #endregion

        #region RemoveCocktail

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void RemoveCocktail_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.RemoveCocktail;
            var action = (Guid performingMemberId) => _club.RemoveCocktail(_cocktail.Id, performingMemberId);
            var assert = () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Cocktails, Has.Count.EqualTo(0));
                    Assert.That(_club.Cocktails.Any(c => c.CocktailId == _cocktail.Id), Is.False);
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
        public void RemoveCocktail_CocktailNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveCocktail(Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The cocktail could not be found in the club. (Parameter 'cocktailId')"));
        }

        [Test]
        public void RemoveCocktail_EmptyCocktailId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveCocktail(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The cocktail could not be found in the club. (Parameter 'cocktailId')"));
        }

        #endregion

        #region RemoveMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void RemoveMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.RemoveMember;
            var action = (Guid performingMemberId) => _club.RemoveMember(_member.Id, performingMemberId);
            var assert = () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Members, Has.Count.EqualTo(1));
                    Assert.That(_club.Members.Any(m => m.Id == _member.Id), Is.False);
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
        public void RemoveMember_MemberNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveMember(Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'clubMemberId')"));
        }

        [Test]
        public void RemoveMember_EmptMemberId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveMember(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'clubMemberId')"));
        }

        #endregion

        #region RemovePermissionToMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void RemovePermissionToMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Add a permission to the member which will be removed
            _club.AddPermissionToMember(_member.Id, ClubPermission.AddCocktail, _owner.Id);

            // Arrange
            var permission = ClubPermission.RemovePermissionToMember;
            var action = (Guid performingMemberId) => _club.RemovePermissionToMember(_member.Id, ClubPermission.AddCocktail, performingMemberId);
            var assert = () =>
            {
                Assert.That(_member.Permissions.Any(p => p == ClubPermission.AddCocktail), Is.False);
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
        public void RemovePermissionToMember_MemberDoesNotHaveThePermission_ThrowArgumentException()
        {
            // Act
            _club.RemovePermissionToMember(_member.Id, ClubPermission.AddCocktail, _owner.Id);

            // Assert
            Assert.That(_member.Permissions, Has.Count.EqualTo(0));
        }

        [Test]
        public void RemovePermissionToMember_MemberNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemovePermissionToMember(Guid.NewGuid(), ClubPermission.AddCocktail, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'clubMemberId')"));
        }

        [Test]
        public void RemovePermissionToMember_EmptMemberId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemovePermissionToMember(Guid.Empty, ClubPermission.AddCocktail, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'clubMemberId')"));
        }

        #endregion

        #region RemoveRolePermission

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void RemoveRolePermission_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Add a permission to the role which will be removed
            _role.AddPermission(ClubPermission.AddCocktail);

            // Arrange
            var permission = ClubPermission.RemoveRolePermission;
            var action = (Guid performingMemberId) => _club.RemoveRolePermission(_role.Id, ClubPermission.AddCocktail, performingMemberId);
            var assert = () =>
            {
                Assert.That(_role.Permissions.Any(p => p == ClubPermission.AddCocktail), Is.False);
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
        public void RemoveRolePermission_RoleDoesNotHaveThePermission_ThrowArgumentException()
        {
            // Act
            _club.RemoveRolePermission(_role.Id, ClubPermission.AddCocktail, _owner.Id);

            // Assert
            Assert.That(_member.Permissions, Has.Count.EqualTo(0));
        }

        [Test]
        public void RemoveRolePermission_RoleNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveRolePermission(Guid.NewGuid(), ClubPermission.AddCocktail, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void RemoveRolePermission_EmptyRoleId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveRolePermission(Guid.Empty, ClubPermission.AddCocktail, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        #endregion

        #region RemoveRoleToMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void RemoveRoleToMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Add role
            var roleName = "New test role";
            _club.CreateRole(roleName, _owner.Id);
            var role = _club.Roles.First(r => r.Name == roleName);
            _member.AddRole(role);

            // Arrange
            var permission = ClubPermission.RemoveRoleToMember;
            var action = (Guid performingMemberId) => _club.RemoveRoleToMember(_member.Id, role.Id, performingMemberId);
            var assert = () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(_member.Roles, Has.Count.EqualTo(1));
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
        public void RemoveRoleToMember_MemberDoesNotHaveRole_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveRoleToMember(_member.Id, Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member doesn't have this role. (Parameter 'roleId')"));
        }

        [Test]
        public void RemoveRoleToMember_MemberNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveRoleToMember(Guid.NewGuid(), _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'clubMemberId')"));
        }

        [Test]
        public void RemoveRoleToMember_EmptMemberId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveRoleToMember(Guid.Empty, _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'clubMemberId')"));
        }

        #endregion

        #region UpdateAddress

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateAddress_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var newAdress = new Address("Test street", "123", "Somewhere", "12345", "", "FR");
            var permission = ClubPermission.ChangeAddress;
            var action = (Guid performingMemberId) => _club.UpdateAddress(newAdress, performingMemberId);
            var assert = () =>
            {
                Assert.That(_club.Address, Is.EqualTo(newAdress));
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
        public void UpdateAddress_NewAdressNull_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _club.UpdateAddress(null, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'newAddress')"));
        }

        #endregion

        #region UpdateDescription

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateDescription_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var newDescription = "A new test description";
            var permission = ClubPermission.ChangeDescription;
            var action = (Guid performingMemberId) => _club.UpdateDescription(newDescription, performingMemberId);
            var assert = () =>
            {
                Assert.That(_club.Description, Is.EqualTo(newDescription));
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
        public void UpdateDescription_NewDescriptionEmpty_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateDescription(string.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'newDescription')"));
        }

        [Test]
        public void UpdateDescription_NewDescriptionNull_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _club.UpdateDescription(null, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'newDescription')"));
        }

        #endregion

        #region UpdateName

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateName_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var newName = "A new test name";
            var permission = ClubPermission.ChangeName;
            var action = (Guid performingMemberId) => _club.UpdateName(newName, performingMemberId);
            var assert = () =>
            {
                Assert.That(_club.Name, Is.EqualTo(newName));
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
        public void UpdateName_NewDescriptionEmpty_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateName(string.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'newName')"));
        }

        [Test]
        public void UpdateName_NewDescriptionNull_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _club.UpdateName(null, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'newName')"));
        }

        #endregion

        #region UpdateOwner

        [Test]
        public void UpdateOwner_PerformedByOwner_OwnerChanged()
        {
            // Act
            _club.UpdateOwner(_member.Id, _owner.Id);

            // Assert
            Assert.That(_club.Owner, Is.EqualTo(_member.Id));
        }

        [Test]
        public void UpdateOwner_PerformedByMember_ThrowUnauthorizedAccessException()
        {
            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                _club.UpdateOwner(_member.Id, _member.Id));

            Assert.That(exception.Message, Is.EqualTo("Only the Owner of the club can change the ownership."));
        }

        [Test]
        public void UpdateOwner_MemberNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateOwner(Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'newOwnerId')"));
        }

        [Test]
        public void UpdateOwner_EmptyMemberId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateOwner(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club. (Parameter 'newOwnerId')"));
        }

        #endregion

        #region UpdateRoleName

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateRoleName_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var newRoleName = "A new name to the role";
            var permission = ClubPermission.ChangeRoleName;
            var action = (Guid performingMemberId) => _club.UpdateRoleName(newRoleName, _role.Id, performingMemberId);
            var assert = () =>
            {
                Assert.That(_role.Name, Is.EqualTo(newRoleName));
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
        public void UpdateRoleName_RoleNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateRoleName("A new role", Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void UpdateRoleName_EmptyRoleId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateRoleName("A new role", Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club. (Parameter 'roleId')"));
        }

        [Test]
        public void UpdateRoleName_EmptyRoleName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateRoleName(string.Empty, _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'newName')"));
        }

        [Test]
        public void UpdateRoleName_NullRoleName_ThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _club.UpdateRoleName(null, _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'newName')"));
        }

        #endregion

        #region UpdateVisibility

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateVisibility_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermission.ChangeVisibility;
            var action = (Guid performingMemberId) => _club.UpdateVisibility(ClubVisibility.Public, performingMemberId);
            var assert = () =>
            {
                Assert.That(_club.Visibility, Is.EqualTo(ClubVisibility.Public));
            };

            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                assert);
        }

        #endregion
    }
}
