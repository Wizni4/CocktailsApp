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

        private Club ReturnClub() => new ClubBuilder()
            .WithAddress(_address)
            .WithDescription(_description)
            .WithName(_name)
            .WithOwner(_ownerId)
            .WithVisibility(ClubVisibility.Public)
            .Build();

        [Test]
        public void AddCocktail_Owner_CocktailAdded()
        {
            // Arrange
            var club = ReturnClub();

            // Act
            club.AddCocktail(_cocktailId, _ownerId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(club.Cocktails, Has.Count.EqualTo(1));
                Assert.That(club.Cocktails.Any(c => c.CocktailId == _cocktailId), Is.True);
                Assert.That(club.Cocktails.All(c => c.Id != Guid.Empty), Is.True);
                Assert.That(club.Cocktails.All(c => c.CreationDate.Kind == DateTimeKind.Utc), Is.True);
                Assert.That(club.Cocktails.All(c => c.CreationDate <= DateTime.UtcNow), Is.True);
            });
        }

        [Test]
        public void AddCocktail_PermittedMember_CocktailAdded()
        {
            // Arrange
            var club = ReturnClub();
            var newMemberId = Guid.NewGuid();
            club.AddMember(newMemberId, _ownerId);
            var newMember = club.Members.First(m => m.UserId == newMemberId);
            club.AddPermissionToMember(newMember.Id, ClubPermission.AddCocktail, _ownerId);

            // Act
            club.AddCocktail(_cocktailId, newMemberId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(club.Cocktails, Has.Count.EqualTo(1));
                Assert.That(club.Cocktails.Any(c => c.CocktailId == _cocktailId), Is.True);
                Assert.That(club.Cocktails.All(c => c.Id != Guid.Empty), Is.True);
                Assert.That(club.Cocktails.All(c => c.CreationDate.Kind == DateTimeKind.Utc), Is.True);
                Assert.That(club.Cocktails.All(c => c.CreationDate <= DateTime.UtcNow), Is.True);
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


    }
}
