/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.SeedWork
{
    /// <summary>
    /// <see langword="abstract"/> class that represent an object without identity, 
    /// defined by the value of it fields, unlike <see cref="Entity"/>.<br/>
    /// It's a persitent and unmutable <see langword="object"/>.
    /// </summary>
    /// <remarks>
    /// <see langword="operator"/> == and <see langword="operator"/> != are overrided to compare fields values instead of reference.<br/>
    /// Comparison is done using <see langword="abstract"/> method <see cref="GetEqualityComponents"/>, that should be implemented in <see cref="ValueObject"/> children classes.
    /// </remarks>
    public abstract class ValueObject : BaseClass
    {
        /// <summary>
        /// Determines whether two specified <see cref="ValueObject"/> are equals.
        /// </summary>
        /// <remarks>
        /// <see cref="ValueObject"/> are equals when all their properties and their values are equals.
        /// </remarks>
        /// <param name="left"><see cref="ValueObject"/> to the left of <see langword="operator"/> ==</param>
        /// <param name="right"><see cref="ValueObject"/> to the right of <see langword="operator"/> ==</param>
        /// <returns><see langword="true"/> if the <paramref name="left"/> properties and it values are equal to the <paramref name="right"/> properties and it values, otherwise <see langword="false"/>.</returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null && right is null)
                return true;
            else if (left is null || right is null)
                return false;
            else
                return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified <see cref="ValueObject"/> are differents.
        /// </summary>
        /// <remarks>
        /// <see cref="ValueObject"/> are different when at least one of their properties <i>or properties values</i> is different.
        /// </remarks>
        /// <param name="left"><see cref="ValueObject"/> to the left of <see langword="operator"/> !=</param>
        /// <param name="right"><see cref="ValueObject"/> to the right of <see langword="operator"/> !=</param>
        /// <returns><see langword="true"/> if at least one of the <paramref name="left"/> properties <i>or properties values</i> is different to the <paramref name="right"/> properties <i>or properties values</i>, otherwise <see langword="false"/>.</returns>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        /// <summary>
        /// <see langword="abstract"/> method that extract the properties values of <see cref="ValueObject"/> based type.<br/>
        /// Used for <see cref="Equals(object)"/> comparison.
        /// </summary>
        /// <remarks>
        /// Should be implemented in <see cref="ValueObject"/> children classes.
        /// </remarks>
        /// <returns><see cref="IEnumerable{object?}"/> containing all the properties values of a <see cref="ValueObject"/> based type.</returns>
        private protected IEnumerable<object?> GetEqualityComponents()
        {
            // Get all public properties of the object
            return this.GetType()
                .GetProperties()
                .Where(p => p.CanRead)
                .Select(p => p.GetValue(this, null));
        }

        /// <summary>
        /// Compares this <see cref="ValueObject"/> to another object to determine if they are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current <see cref="ValueObject"/>.</param>
        /// <returns>
        /// <see langword="true"/> if the current object and the <paramref name="obj"/> are of the same type and their equality components are the same; otherwise <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// This method overrides the default <see cref="Equals"/> method to provide a value-based equality comparison for value objects.<br/>
        /// The comparison is based on the equality of the components defined by the <see cref="GetEqualityComponents"/> method.
        /// </remarks>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Calculates the hash code for this <see cref="ValueObject"/> based on its equality components.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        /// <remarks>
        /// This method overrides the default <see cref="GetHashCode"/> method to generate a hash code that is based on the equality components of the value object.
        /// </remarks>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
