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
        private Guid _memberUserId = Guid.NewGuid();
        private Guid _ownerUserId = Guid.NewGuid();
        private Club _club;
        private ClubMember _member;
        private ClubMember _owner;
        private ClubRole _ownerRole;
        private ClubRole _role;
        private ClubCocktail _cocktail;

        [SetUp]
        public void SetUp()
        {
            _club = new ClubBuilder()
                .WithAddress(new("123 Main St", "456", "Springfield", "12345", "IL", "USA"))
                .WithDescription("A club for testing.")
                .WithName("Test Club")
                .WithOwner(_ownerUserId)
                .WithVisibility(ClubVisibility.Public)
                .Build();

            // Set the owner
            _owner = _club.Members.First();

            // Set the owner role id
            _ownerRole = _club.Roles.First();

            // Create Test role
            var roleName = "Test Role";
            _club.CreateRole(roleName, _owner.Id);
            _role = _club.Roles.First(r => r.Name == roleName);

            // Create Test Member
            _club.AddMember(_memberUserId, _owner.Id);
            _member = _club.Members.First(m => m.UserId == _memberUserId);
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
                //  new TestCaseData(scenario        , shouldSucceed),
                    new TestCaseData("owner"         , true         ),
                    new TestCaseData("user"          , true         ),
                    new TestCaseData("member"        , true         ),
                    new TestCaseData("not_permitted" , false        ),
                    new TestCaseData("not_a_member"  , false        ),
                    new TestCaseData("empty_actor_id", false        ),
                ];
            }
        }

        private Guid SetupScenario(ClubPermissionType permission, string scenario)
        {
            var actorId = _member.Id;

            switch (scenario)
            {
                case "owner":
                    actorId = _owner.Id;
                    break;
                case "user":
                    _role.AddPermission(permission);
                    break;
                case "member":
                    _role.AddPermission(permission);
                    break;
                case "not_a_member":
                    _club.RemoveMember(_member.Id, _owner.Id);
                    break;
                case "empty_actor_id":
                    actorId = Guid.Empty;
                    break;
                default: // Not permitted -> nothing to do by default not permitted
                    break;
            }

            return actorId;
        }
        private void RunPermissionScenarios(
            ClubPermissionType permission,
            string scenario,
            bool shouldSucceed,
            Action<Guid> action,
            Action assert
        )
        {
            var actorId = SetupScenario(permission, scenario);

            if (shouldSucceed)
            {
                action(actorId);
                assert();
            }
            else
            {
                var ex = Assert.Throws<UnauthorizedAccessException>(() => action(actorId));
                Assert.That(ex.Message, Is.EqualTo($"Club member does not have the permission to {permission}"));
            }
        }

        private void RunPermissionScenarios<TResult>(
            ClubPermissionType permission,
            string scenario,
            bool shouldSucceed,
            Func<Guid, TResult> action,
            Action<TResult> assert
         )
        {
            var actorId = SetupScenario(permission, scenario);

            if (shouldSucceed)
            {
                var result = action(actorId);
                assert(result);
            }
            else
            {
                var ex = Assert.Throws<UnauthorizedAccessException>(() => action(actorId));
                Assert.That(ex.Message, Is.EqualTo($"Club member does not have the permission to {permission}"));
            }
        }

        #endregion

        #region AddCocktail

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddCocktail_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermissionType.AddCocktail;
            var cocktailId = Guid.NewGuid();
            var action = (Guid actorId) => _club.AddCocktail(cocktailId, actorId);

            // Assert on success (has permissions)
            void Assert(ClubCocktail newCocktail)
            {
                // Assert
                NUnit.Framework.Assert.Multiple(() =>
                {
                    NUnit.Framework.Assert.That(_club.Cocktails, Has.Count.EqualTo(2));
                    NUnit.Framework.Assert.That(_club.Cocktails.Any(c => c.Id == newCocktail.Id), Is.True);
                    NUnit.Framework.Assert.That(_club.Cocktails.Any(c => c.CocktailId == cocktailId), Is.True);
                    NUnit.Framework.Assert.That(newCocktail.CocktailId, Is.EqualTo(cocktailId));
                    NUnit.Framework.Assert.That(newCocktail.Id, Is.Not.EqualTo(Guid.Empty));
                    NUnit.Framework.Assert.That(newCocktail.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                    NUnit.Framework.Assert.That(newCocktail.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                    NUnit.Framework.Assert.That(newCocktail.UpdateDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                    NUnit.Framework.Assert.That(newCocktail.UpdateDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                });
            }
            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                Assert);
        }

        [Test]
        public void AddCocktail_EmptyCocktailId_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddCocktail(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("CocktailId cannot be null."));
        }

        [Test]
        public void AddCocktail_CocktailAlreadyInClub_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddCocktail(_cocktail.CocktailId, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("This cocktail is already in the club."));
        }

        #endregion

        #region AddMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermissionType.AddMember;
            var userId = Guid.NewGuid();
            var action = (Guid actorId) => _club.AddMember(userId, actorId);

            // Assert on success (has permissions)
            void Assert(ClubMember newMember)
            {
                // Assert
                NUnit.Framework.Assert.Multiple(() =>
                {
                    NUnit.Framework.Assert.That(_club.Members, Has.Count.EqualTo(3));
                    NUnit.Framework.Assert.That(_club.Members.Any(m => m.Id == newMember.Id), Is.True);
                    NUnit.Framework.Assert.That(_club.Members.Any(m => m.UserId == userId), Is.True);
                    NUnit.Framework.Assert.That(newMember.UserId, Is.EqualTo(userId));
                    NUnit.Framework.Assert.That(newMember.Id, Is.Not.EqualTo(Guid.Empty));
                    NUnit.Framework.Assert.That(newMember.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                    NUnit.Framework.Assert.That(newMember.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                    NUnit.Framework.Assert.That(newMember.UpdateDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                    NUnit.Framework.Assert.That(newMember.UpdateDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                });
            }
            ;
            // Act & Assert
            RunPermissionScenarios(
                permission,
                scenario,
                shouldSucceed,
                action,
                Assert);
        }

        [Test]
        public void AddMember_EmptyMemberId_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddMember(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("UserId cannot be null."));
        }

        [Test]
        public void AddMember_MemberAlreadyInClub_ThrowsUnauthorizedAccessException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddMember(_member.UserId, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("This user is already a member of the club."));
        }

        #endregion

        #region AddRolePermission

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void AddRolePermission_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermissionType.AddPermissionToRole;
            var permissionAdded = ClubPermissionType.RemoveRoleFromMember;
            var action = (Guid actorId) => _club.AddPermissionToRole(_role.Id, permissionAdded, actorId);

            // Assert on success (has permissions)
            var assert = () => Assert.That(_role.Permissions.Any(p => p.Permission == permissionAdded), Is.True);

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
            var permission = ClubPermissionType.RemoveRoleFromMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddPermissionToRole(Guid.NewGuid(), permission, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club."));
        }

        [Test]
        public void AddRolePermission_EmptyRoleId_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermissionType.RemoveRoleFromMember;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddPermissionToRole(Guid.Empty, permission, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club."));
        }

        [Test]
        public void AddRolePermission_RoleAlreadyHasPermission_ThrowsArgumentException()
        {
            // Arrange
            var permission = ClubPermissionType.RemoveRoleFromMember;
            _role.AddPermission(permission);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddPermissionToRole(_role.Id, permission, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role already has the permission."));
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
            var permission = ClubPermissionType.AddRoleToMember;
            var action = (Guid actorId) => _club.AddRoleToMember(_member.Id, role.Id, actorId);

            // Assert on success (has permissions)
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
        public void AddRoleToMember_OwnerRoleActorNotOwner_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            _role.AddPermission(ClubPermissionType.AddRoleToMember);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                _club.AddRoleToMember(_member.Id, _ownerRole.Id, _member.Id));

            Assert.That(exception.Message, Is.EqualTo("Only member with the Owner role can grant the Owner role to another member."));
        }

        [Test]
        public void AddRoleToMember_MemberNotInClub_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRoleToMember(Guid.NewGuid(), _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club."));
        }

        [Test]
        public void AddRoleToMember_RoleNotInClub_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRoleToMember(_member.Id, Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club."));
        }

        [Test]
        public void AddRoleToMember_MemberAlreadyHasRole_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.AddRoleToMember(_member.Id, _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member already has this role."));
        }

        #endregion

        #region CreateRole

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void CreateRole_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermissionType.CreateRole;
            var roleName = "Test role";
            var action = (Guid actorId) => _club.CreateRole(roleName, actorId);
            void Assert(ClubRole newRole)
            {
                NUnit.Framework.Assert.Multiple(() =>
                {
                    NUnit.Framework.Assert.That(_club.Roles, Has.Count.EqualTo(3));
                    NUnit.Framework.Assert.That(_club.Roles.Any(r => r.Id == newRole.Id), Is.True);
                    NUnit.Framework.Assert.That(_club.Roles, Has.Count.EqualTo(3));
                    NUnit.Framework.Assert.That(newRole.Name, Is.EqualTo(roleName));
                    NUnit.Framework.Assert.That(newRole.Id, Is.Not.EqualTo(Guid.Empty));
                    NUnit.Framework.Assert.That(newRole.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                    NUnit.Framework.Assert.That(newRole.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                });
            }
            ;

            // Act & Assert
            RunPermissionScenarios<ClubRole>(
                permission,
                scenario,
                shouldSucceed,
                action,
                Assert);
        }

        [Test]
        public void CreateRole_EmptyName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.CreateRole(string.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role name cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void CreateRole_NullName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.CreateRole(null, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role name cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void CreateRole_WitheSpaceName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.CreateRole("  ", _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role name cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void CreateRole_RoleAlreadyInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.CreateRole(_role.Name, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("This role name already exists."));
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
            var permission = ClubPermissionType.DeleteRole;
            var action = (Guid actorId) => _club.DeleteRole(role.Id, actorId);
            var assert = () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Roles, Has.Count.EqualTo(2));
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
        public void DeleteRole_OwnerRole_ThrowUnauthorizedAccessException()
        {
            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                _club.DeleteRole(_ownerRole.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("Owner role can't be removed from the club."));
        }

        [Test]
        public void DeleteRole_RoleNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.DeleteRole(Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club."));
        }

        [Test]
        public void DeleteRole_EmptyRoleId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.DeleteRole(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club."));
        }

        #endregion

        #region RemoveCocktail

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void RemoveCocktail_ClubCocktaiId_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermissionType.RemoveCocktail;
            var action = (Guid actorId) => _club.RemoveCocktail(_cocktail.Id, actorId);
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

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void RemoveCocktail_ClubCocktaiCocktailId_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermissionType.RemoveCocktail;
            var action = (Guid actorId) => _club.RemoveCocktail(_cocktail.CocktailId, actorId);
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

            Assert.That(exception.Message, Is.EqualTo("The cocktail could not be found in the club."));
        }

        [Test]
        public void RemoveCocktail_EmptyCocktailId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveCocktail(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The cocktail could not be found in the club."));
        }

        #endregion

        #region RemoveMember

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void RemoveMember_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var memberToRemove = _club.AddMember(Guid.NewGuid(), _owner.Id);
            var permission = ClubPermissionType.RemoveMember;
            var action = (Guid actorId) => _club.RemoveMember(memberToRemove.Id, actorId);
            var assert = () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(_club.Members, Has.Count.EqualTo(2));
                    Assert.That(_club.Members.Any(m => m.Id == memberToRemove.Id), Is.False);
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
        public void RemoveMember_RemoveOwner_ThrowUnauthorizedAccessException()
        {
            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                _club.RemoveMember(_owner.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The owners of the club can't be removed from the club"));
        }

        [Test]
        public void RemoveMember_RemoveHimself_ThrowUnauthorizedAccessException()
        {
            // Arrange
            _role.AddPermission(ClubPermissionType.RemoveMember);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                _club.RemoveMember(_member.Id, _member.Id));

            Assert.That(exception.Message, Is.EqualTo("A member can't remove him self from a club."));
        }

        [Test]
        public void RemoveMember_MemberNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveMember(Guid.NewGuid(), _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club."));
        }

        [Test]
        public void RemoveMember_EmptMemberId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveMember(Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club."));
        }

        #endregion

        #region RemovePermissionFromRole

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void RemovePermissionFromRole_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Add a permission to the role which will be removed
            _role.AddPermission(ClubPermissionType.AddCocktail);

            // Arrange
            var permission = ClubPermissionType.RemovePermissionFromRole;
            var action = (Guid actorId) => _club.RemovePermissionFromRole(_role.Id, ClubPermissionType.AddCocktail, actorId);
            var assert = () =>
            {
                Assert.That(_role.Permissions.Any(p => p.Permission == ClubPermissionType.AddCocktail), Is.False);
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
        public void RemovePermissionFromRole_OwnerRole_ThrowUnauthorizedAccessException()
        {
            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                _club.RemovePermissionFromRole(_ownerRole.Id, ClubPermissionType.AddCocktail, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("Permissions cannot be removed from the Owner role."));
        }

        [Test]
        public void RemovePermissionFromRole_RoleDoesNotHaveThePermission_ThrowKeyNotFoundException()
        {
            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() =>
                _club.RemovePermissionFromRole(_role.Id, ClubPermissionType.AddCocktail, _owner.Id));

            // Assert
            Assert.That(exception.Message, Is.EqualTo("Permission 'AddCocktail' was not found in the 'Test Role' role."));
        }

        [Test]
        public void RemovePermissionFromRole_RoleNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemovePermissionFromRole(Guid.NewGuid(), ClubPermissionType.AddCocktail, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club."));
        }

        [Test]
        public void RemovePermissionFromRole_EmptyRoleId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemovePermissionFromRole(Guid.Empty, ClubPermissionType.AddCocktail, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club."));
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
            var permission = ClubPermissionType.RemoveRoleFromMember;
            var action = (Guid actorId) => _club.RemoveRoleFromMember(_member.Id, role.Id, actorId);
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
        public void RemoveRoleToMember_OwnerRoleActorNotOwner_ThrowUnauthorizedAccessException()
        {
            // Arrange
            _role.AddPermission(ClubPermissionType.RemoveRoleFromMember);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                _club.RemoveRoleFromMember(_owner.Id, _ownerRole.Id, _member.Id));

            Assert.That(exception.Message, Is.EqualTo("The owner of the club can't be removed from the owner role."));
        }

        [Test]
        public void RemoveRoleToMember_NoMemberLeftInOwnerRole_ThrowUnauthorizedAccessException()
        {
            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                _club.RemoveRoleFromMember(_owner.Id, _ownerRole.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("At least one member must have the Owner role."));
        }

        [Test]
        public void RemoveRoleToMember_MemberDoesNotHaveRole_ThrowArgumentException()
        {
            // Arrange
            _member.RemoveRole(_role.Id);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveRoleFromMember(_member.Id, _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member doesn't have this role."));
        }

        [Test]
        public void RemoveRoleToMember_MemberNotInClub_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveRoleFromMember(Guid.NewGuid(), _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club."));
        }

        [Test]
        public void RemoveRoleToMember_EmptMemberId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.RemoveRoleFromMember(Guid.Empty, _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The member could not be found in the club."));
        }

        #endregion

        #region UpdateAddress

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateAddress_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var newAdress = new Address("Test street", "123", "Somewhere", "12345", "", "FR");
            var permission = ClubPermissionType.ChangeAddress;
            var action = (Guid actorId) => _club.UpdateAddress(newAdress, actorId);
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
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateAddress(null, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The address cannot be null."));
        }

        #endregion

        #region UpdateDescription

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateDescription_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var newDescription = "A new test description";
            var permission = ClubPermissionType.ChangeDescription;
            var action = (Guid actorId) => _club.UpdateDescription(newDescription, actorId);
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
        public void UpdateDescription_WhitespaceDescriptiopn_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateDescription(" ", _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The club description cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void UpdateDescription_EmptyDescriptiopn_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateDescription(string.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The club description cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void UpdateDescription_NullDescription_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateDescription(null, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The club description cannot be an empty string or composed entirely of whitespace."));
        }

        #endregion

        #region UpdateName

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateName_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var newName = "A new test name";
            var permission = ClubPermissionType.ChangeName;
            var action = (Guid actorId) => _club.UpdateName(newName, actorId);
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
        public void UpdateName_WhitespaceName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateName(string.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The club name cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void UpdateName_EmptyName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateName(string.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The club name cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void UpdateName_NullName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateName(null, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The club name cannot be an empty string or composed entirely of whitespace."));
        }

        #endregion

        #region UpdateRoleName

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateRoleName_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var newRoleName = "A new name to the role";
            var permission = ClubPermissionType.ChangeRoleName;
            var action = (Guid actorId) => _club.UpdateRoleName(newRoleName, _role.Id, actorId);
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

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club."));
        }

        [Test]
        public void UpdateRoleName_EmptyRoleId_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateRoleName("A new role", Guid.Empty, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role could not be found in the club."));
        }

        [Test]
        public void UpdateRoleName_EmptyRoleName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateRoleName(string.Empty, _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role name cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void UpdateRoleName_NullRoleName_ThrowArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _club.UpdateRoleName(null, _role.Id, _owner.Id));

            Assert.That(exception.Message, Is.EqualTo("The role name cannot be an empty string or composed entirely of whitespace."));
        }

        #endregion

        #region UpdateVisibility

        [Test, TestCaseSource(nameof(PermissionTestCases))]
        public void UpdateVisibility_PermissionScenarios(string scenario, bool shouldSucceed)
        {
            // Arrange
            var permission = ClubPermissionType.ChangeVisibility;
            var action = (Guid actorId) => _club.UpdateVisibility(ClubVisibility.Public, actorId);
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
