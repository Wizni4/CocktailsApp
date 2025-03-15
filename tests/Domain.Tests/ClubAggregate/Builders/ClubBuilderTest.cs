/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

using Moq;

using Newtonsoft.Json.Linq;

using System.Reflection.Metadata;

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
                .Build();

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(club.Address, Is.EqualTo(_address));
                Assert.That(club.Description, Is.EqualTo(_description));
                Assert.That(club.Name, Is.EqualTo(_name));
                Assert.That(club.Owner.UserId, Is.EqualTo(_ownerId));
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
    }
}
