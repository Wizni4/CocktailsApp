/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace CocktailsApp.Domain.SeedWork
{
    /// <summary>
    /// Represents a specification pattern that defines a condition or set of conditions that an object of type <typeparamref name="T"/> must satisfy.
    /// <br></br>
    /// The specification can be used to encapsulate business rules or criteria for filtering or validating entities.
    /// </summary>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Gets the expression that defines the specification's criteria.
        /// </summary>
        /// <value>
        /// An expression representing the condition or business rule to be satisfied by an object of type <typeparamref name="T"/>.
        /// </value>
        Expression<Func<T, bool>> SpecExpression { get; }

        /// <summary>
        /// Determines whether the specified object satisfies the specification.
        /// </summary>
        /// <param name="obj">The object of type <typeparamref name="T"/> to evaluate.</param>
        /// <returns><see langword="true"/> if the object satisfies the specification; otherwise, <see langword="false"/>.</returns>
        bool IsSatisfiedBy(T obj);
    }
}
