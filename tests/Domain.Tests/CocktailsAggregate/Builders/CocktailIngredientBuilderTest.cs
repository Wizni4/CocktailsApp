using Domain.CocktailAggregate;

using FluentAssertions;

namespace Domain.Tests.CocktailsAggregate
{
    public class CocktailIngredientBuilderTest
    {
        [Test]
        public void AddName_WithValidName_SetsNameCorrectly()
        {

            // Arrange
            var Name = "Mojito"; // Valid name to set

            // Act
            var builder = new CocktailBuilder().AddName(Name);

            // Assert
            // Since the CocktailBuilder has no getter for the _name, 
            // we need to check if the internal state has been updated correctly.
            // We can use reflection to access private fields for testing.
            var nameField = typeof(CocktailBuilder)
                .GetField("_name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualName = nameField?.GetValue(builder);

            actualName.Should().Be(Name);
        }
    }
}
