/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;
using CocktailsApp.Domain.Tests.SeedWork;
using CocktailsApp.Domain.UserAggregate;

using NUnit.Framework;

using System.Security;

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
        private readonly Guid _ownerId = Guid.NewGuid();
        private readonly Guid _cocktailId = Guid.NewGuid();
        private readonly Guid _memberId = Guid.NewGuid();

        private Club ReturnClub() => new ClubBuilder()
            .WithAddress(_address)
            .WithDescription(_description)
            .WithName(_name)
            .WithOwner(_ownerId)
            .WithVisibility(ClubVisibility.Public)
            .Build();

        #region AddCocktail

        [Test]
        public void AddCocktail_Owner_CocktailAdded()
        {
            // Arrange
            var club = ReturnClub();

            // Act
            club.AddCocktail(_cocktailId, _ownerId);

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
            club.AddMember(_memberId, _ownerId);
            var newMember = club.Members.First(m => m.UserId == _memberId);
            club.AddPermissionToMember(newMember.Id, ClubPermission.AddCocktail, _ownerId);

            // Act
            club.AddCocktail(_cocktailId, _memberId);

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
            club.AddMember(newMemberId, _ownerId);
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
            club.AddCocktail(_cocktailId, _ownerId);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                club.AddCocktail(_cocktailId, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("This cocktail is already in the club. (Parameter 'cocktailId')"));
        }

        [Test]
        public void AddCocktail_EmptyCocktailId_ThrowsArgumentNullException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                club.AddCocktail(Guid.Empty, _ownerId));

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
            club.AddMember(_memberId, _ownerId);

            // Assert
            var newMember = club.Members.First(m => m.UserId == _memberId);
            Assert.Multiple(() =>
            {
                Assert.That(club.Members, Has.Count.EqualTo(2));
                Assert.That(newMember.UserId, Is.EqualTo(_memberId));
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
            club.AddMember(memberId, _ownerId);
            var member = club.Members.First(m => m.UserId == memberId);
            club.AddPermissionToMember(member.Id, ClubPermission.AddMember, _ownerId);

            // Act
            club.AddMember(_memberId, memberId);

            // Assert
            var newMember = club.Members.First(m => m.UserId == _memberId);
            Assert.Multiple(() =>
            {
                Assert.That(club.Members, Has.Count.EqualTo(3));
                Assert.That(newMember.UserId, Is.EqualTo(_memberId));
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
            club.AddMember(memberId, _ownerId);
            var member = club.Members.First(m => m.UserId == memberId);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddMember(_memberId, member.Id));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddMember"));
        }

        [Test]
        public void AddMember_NotAMember_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddMember(_memberId, Guid.NewGuid()));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddMember"));
        }

        [Test]
        public void AddMember_MemberAlreadyInClub_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();
            club.AddMember(_memberId, _ownerId);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                club.AddMember(_memberId, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("This member is already in the club. (Parameter 'memberId')"));
        }

        [Test]
        public void AddMember_EmptyMemberId_ThrowsArgumentNullException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                club.AddMember(Guid.Empty, _ownerId));

            Assert.That(exception.Message, Is.EqualTo("UserId cannot be null. (Parameter 'userId')"));
        }

        [Test]
        public void AddMember_EmptyUserId_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var club = ReturnClub();

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() =>
                club.AddMember(_memberId, Guid.Empty));

            Assert.That(exception.Message, Is.EqualTo("User does not have permission to AddMember"));
        }

        #endregion

    }
}
