/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */


namespace CocktailsApp.Domain.Tests.SeedWork
{
    public abstract class EntityTest : BaseClassTest
    {
        protected abstract Entity ReturnLeftEntity();
        protected abstract Entity ReturnRightEntity();

        [Test]
        public void Id_ShouldBeUnique()
        {
            // Arrange
            var leftEntity = ReturnLeftEntity();
            var rightEntity = ReturnRightEntity();

            // Act & Assert
            Assert.That(rightEntity.Id, Is.Not.EqualTo(leftEntity.Id));
        }

        [Test]
        public void CreationDate_ShouldBeSetToUtcNow()
        {
            // Arrange
            var entity = ReturnLeftEntity();

            // Act & Assert
            Assert.That(entity.CreationDate.Kind, Is.EqualTo(DateTimeKind.Utc));
            Assert.LessOrEqual(entity.CreationDate, DateTime.UtcNow);
        }
    }
}
