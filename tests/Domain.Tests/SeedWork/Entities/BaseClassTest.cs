/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */


namespace CocktailsApp.Domain.Tests.SeedWork
{
    public abstract class BaseClassTest
    {
        protected abstract BaseClass ReturnBaseClass();

        [Test]
        public void Type_ShouldReturnDerivedClassName()
        {
            // Arrange
            var baseClass = ReturnBaseClass();

            // Act
            var typeName = baseClass.GetTypeName();

            // Assert
            Assert.Equals(baseClass.GetType().Name, typeName);
        }
    }

    public static class BaseClassExtensions
    {
        public static string GetTypeName(this BaseClass baseClass)
        {
            // Use reflection to access the private protected property
            var propertyInfo = typeof(BaseClass).GetProperty("Type",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (string)propertyInfo!.GetValue(baseClass)!;
        }
    }
}
