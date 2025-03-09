using FluentAssertions;
using Domain.CocktailAggregate;

namespace Domain.Tests.CocktailsAggregate
{
    public class CocktailIngredientBuilderTest
    {
        [Test]
        public void AddName_WithValidName_ReturnCocktail()
        {

            // Arrange
            var name = "Mojito"; // Valid name to set

            // Act
            var builder = new CocktailBuilder().AddName(name);

            // Assert
            // Since the CocktailBuilder has no getter for the _name, 
            // we need to check if the internal state has been updated correctly.
            // We can use reflection to access private fields for testing.
            var nameField = typeof(CocktailBuilder)
                .GetField("_name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualName = nameField.GetValue(builder);

            actualName.Should().Be(name);
        }

        [Test]
        public void AddName_WithEmptyName_ThrowsArgumentException()
        {
            // Arrange
            var emptyName = ""; // Empty name input

            // Act & Assert
            Action act = () => new CocktailBuilder().AddName(emptyName);
            act.Should().Throw<ArgumentException>().WithMessage("Name cannot be empty");
        }
    }
}
