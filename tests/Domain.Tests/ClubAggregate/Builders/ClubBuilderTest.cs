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
    public class ClubBuilderTest
    {
        private readonly Address _address = new("123 Main St", "456", "Springfield", "12345", "IL", "USA");
        private readonly string _description = "A club for testing.";
        private readonly string _name = "Test Club";
        private readonly Guid _ownerId = Guid.NewGuid();

        [Test]
        public void Build_ValidAttributes_ReturnsClub()
        {
            // Act
            var club = new ClubBuilder()
                .WithAddress(_address)
                .WithDescription(_description)
                .WithName(_name)
                .WithOwner(_ownerId)
                .WithVisibility(ClubVisibility.Public)
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(club.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(club.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
                Assert.That(club.CreationDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                Assert.That(club.Address, Is.EqualTo(_address));
                Assert.That(club.Description, Is.EqualTo(_description));
                Assert.That(club.Name, Is.EqualTo(_name));
                Assert.That(club.Owner, Is.EqualTo(_ownerId));
                Assert.That(club.Members, Has.Count.EqualTo(1));
                Assert.That(club.Members.Any(m => m.UserId == _ownerId), Is.True);
                Assert.That(club.Visibility, Is.EqualTo(ClubVisibility.Public));
            });
        }

        [Test]
        public void Build_WithoutAddress_ThrowsArgumentNullException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ClubBuilder()
                    .WithDescription(_description)
                    .WithName(_name)
                    .WithOwner(_ownerId)
                    .Build());

            // Assert
            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'newAddress')"));
        }

        [Test]
        public void Build_WithoutDescription_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ClubBuilder()
                    .WithAddress(_address)
                    .WithName(_name)
                    .WithOwner(_ownerId)
                    .Build());

            Assert.That(exception.Message, Does.Contain("Value cannot be null. (Parameter 'newDescription')"));
        }

        [Test]
        public void Build_WithoutName_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ClubBuilder()
                    .WithAddress(_address)
                    .WithDescription(_description)
                    .WithOwner(_ownerId)
                    .Build());

            Assert.That(exception.Message, Does.Contain("Value cannot be null. (Parameter 'newName')"));
        }

        [Test]
        public void Build_WithoutOwnerId_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ClubBuilder()
                    .WithAddress(_address)
                    .WithDescription(_description)
                    .WithName(_name)
                    .Build());

            Assert.That(exception.Message, Does.Contain("UserId cannot be null. (Parameter 'userId')"));
        }

        [Test]
        public void Build_WithoutVisibility_ReturnsPrivateClub()
        {
            // Act
            var club = new ClubBuilder()
                .WithAddress(_address)
                .WithDescription(_description)
                .WithName(_name)
                .WithOwner(_ownerId)
                .Build();

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(club.Address, Is.EqualTo(_address));
                Assert.That(club.Description, Is.EqualTo(_description));
                Assert.That(club.Name, Is.EqualTo(_name));
                Assert.That(club.Owner, Is.EqualTo(_ownerId));
                Assert.That(club.Members, Has.Count.EqualTo(1));
                Assert.That(club.Members.Any(m => m.UserId == _ownerId), Is.True);
                Assert.That(club.Visibility, Is.EqualTo(ClubVisibility.Private));
            });
        }
    }
}
