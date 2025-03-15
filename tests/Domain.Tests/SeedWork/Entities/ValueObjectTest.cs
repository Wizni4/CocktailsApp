/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */


namespace CocktailsApp.Domain.Tests.SeedWork
{
    public abstract class ValueObjectTest
    {
        protected abstract ValueObject ReturnLeft();
        protected abstract ValueObject ReturnRight();
        protected abstract IEnumerable<object> ReturnListExpected();

        #region Equal Operator

        [Test]
        public void EqualOperator_LeftAndRightNull_ReturnTrue()
        {
            //Act
            var actual = (ValueObject)null == (ValueObject)null;

            //Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void EqualOperator_LeftNull_ReturnFalse()
        {
            //Arrange
            var right = ReturnRight();

            //Act
            var actual = null == right;

            //Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void EqualOperator_RightNull_ReturnFalse()
        {
            //Arrange
            var left = ReturnLeft();

            //Act
            var actual = left == null;

            //Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void EqualOperator_NotSame_ReturnFalse()
        {
            //Arrange
            var right = ReturnRight();
            var left = ReturnLeft();

            //Act
            var actual = left == right;

            //Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void EqualOperator_Same_ReturnTrue()
        {
            //Arrange
            var right = ReturnLeft();
            var left = ReturnLeft();

            //Act
            var actual = left == right;

            //Assert
            Assert.That(actual, Is.True);
        }

        #endregion

        #region Not Equal Operator

        [Test]
        public void NotEqualOperator_LeftAndRightNull_ReturnFalse()
        {
            //Act
            var actual = (ValueObject)null != (ValueObject)null;

            //Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void NotEqualOperator_LeftNull_ReturnTrue()
        {
            //Arrange
            var right = ReturnRight();

            //Act
            var actual = null != right;

            //Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void NotEqualOperator_RightNull_ReturnTrue()
        {
            //Arrange
            var left = ReturnLeft();

            //Act
            var actual = left != null;

            //Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void NotEqualOperator_NotSame_ReturnTrue()
        {
            //Arrange
            var right = ReturnRight();
            var left = ReturnLeft();

            //Act
            var actual = left != right;

            //Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void NotEqualOperator_Same_ReturnFalse()
        {
            //Arrange
            var right = ReturnLeft();
            var left = ReturnLeft();

            //Act
            var actual = left != right;

            //Assert
            Assert.That(actual, Is.False);
        }

        #endregion

        #region Equal

        [Test]
        public void Equal_ObjNull_ReturnFalse()
        {
            //Arrange
            var valueObject1 = ReturnLeft();

            //Act
            var actual = valueObject1.Equals(null);

            //Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Equal_NotSame_ReturnFalse()
        {
            //Arrange
            var valueObject1 = ReturnLeft();
            var valueObject2 = ReturnRight();

            //Act
            var actual = valueObject1.Equals(valueObject2);

            //Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Equal_Same_ReturnTrue()
        {
            //Arrange
            var valueObject1 = ReturnLeft();
            var valueObject2 = ReturnLeft();

            //Act
            var actual = valueObject1.Equals(valueObject2);

            //Assert
            Assert.That(actual, Is.True);
        }

        #endregion
    }
}
