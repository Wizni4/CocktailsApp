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
                Assert.That(club.Members, Has.Count.EqualTo(1));
                Assert.That(club.Members.Any(m => m.UserId == _ownerId), Is.True);
                Assert.That(club.Visibility, Is.EqualTo(ClubVisibility.Public));
            });
        }

        [Test]
        public void Build_WithoutAddress_ThrowsArgumentException()
        {
            // Act
            var exception = Assert.Throws<ArgumentException>(() =>
                new ClubBuilder()
                    .WithDescription(_description)
                    .WithName(_name)
                    .WithOwner(_ownerId)
                    .Build());

            // Assert
            Assert.That(exception.Message, Is.EqualTo("The address cannot be null."));
        }

        [Test]
        public void Build_WithoutDescription_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new ClubBuilder()
                    .WithAddress(_address)
                    .WithName(_name)
                    .WithOwner(_ownerId)
                    .Build());

            Assert.That(exception.Message, Does.Contain("The club description cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void Build_WithoutName_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new ClubBuilder()
                    .WithAddress(_address)
                    .WithDescription(_description)
                    .WithOwner(_ownerId)
                    .Build());

            Assert.That(exception.Message, Does.Contain("The club name cannot be an empty string or composed entirely of whitespace."));
        }

        [Test]
        public void Build_WithoutOwnerId_ThrowsArgumentException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new ClubBuilder()
                    .WithAddress(_address)
                    .WithDescription(_description)
                    .WithName(_name)
                    .Build());

            Assert.That(exception.Message, Does.Contain("User creating entity must be specified"));
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
                Assert.That(club.Members, Has.Count.EqualTo(1));
                Assert.That(club.Members.Any(m => m.UserId == _ownerId), Is.True);
                Assert.That(club.Visibility, Is.EqualTo(ClubVisibility.Private));
            });
        }
    }
}
