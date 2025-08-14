/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Common
{
    /// <summary>
    /// Provides extension methods for the <see cref="ISpecification{T}"/> interface to allow easy composition of specifications using logical operations such as AND, OR, and NOT.
    /// </summary>
    public static class SpecificationExtensions
    {
        /// <summary>
        /// Combines two specifications using a logical AND.<br/>
        /// The resulting specification will be satisfied if both the left and right specifications are satisfied.
        /// </summary>
        /// <typeparam name="T">The type of the entity that the specifications apply to.</typeparam>
        /// <param name="left">The left specification to be combined.</param>
        /// <param name="right">The right specification to be combined.</param>
        /// <returns>A new specification that is the logical AND of the two specifications.</returns>
        public static ISpecification<T> And<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            return new And<T>(left, right);
        }

        /// <summary>
        /// Combines two specifications using a logical OR. 
        /// The resulting specification will be satisfied if either the left or the right specification is satisfied.
        /// </summary>
        /// <typeparam name="T">The type of the entity that the specifications apply to.</typeparam>
        /// <param name="left">The left specification to be combined.</param>
        /// <param name="right">The right specification to be combined.</param>
        /// <returns>A new specification that is the logical OR of the two specifications.</returns>
        public static ISpecification<T> Or<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            return new Or<T>(left, right);
        }

        /// <summary>
        /// Negates a specification. 
        /// The resulting specification will be satisfied if the inner specification is not satisfied.
        /// </summary>
        /// <typeparam name="T">The type of the entity that the specification applies to.</typeparam>
        /// <param name="inner">The specification to be negated.</param>
        /// <returns>A new specification that represents the negation of the inner specification.</returns>
        public static ISpecification<T> Negate<T>(this ISpecification<T> inner)
        {
            return new Negate<T>(inner);
        }
    }
}
