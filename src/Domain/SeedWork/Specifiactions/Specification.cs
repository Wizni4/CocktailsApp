/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace Domain.SeedWork
{
    /// <summary>
    /// Represents a base class for implementing the specification pattern.<br/>
    /// 
    /// Provides a common implementation for the <see cref="ISpecification{T}"/> interface,
    /// allowing the creation of specifications that can be checked against objects of type <typeparamref name="T"/>.<br/>
    /// 
    /// This abstract class encapsulates the logic to evaluate whether an object satisfies the conditions defined in the specification.
    /// </summary>
    public abstract class Specification<T> : ISpecification<T>
    {
        private Func<T, bool>? _compiledExpression;

        /// <summary>
        /// Gets the compiled version of the specification expression.
        /// </summary>
        /// <value>
        /// A compiled version of the specification's condition, which can be invoked 
        /// to determine whether an object of type <typeparamref name="T"/> satisfies the criteria.
        /// </value>
        private Func<T, bool> CompiledExpression
        {
            get { return _compiledExpression ??= SpecExpression.Compile(); }
        }

        /// <inheritdoc cref="ISpecification{T}.SpecExpression"/>
        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        /// <inheritdoc cref="ISpecification{T}.IsSatisfiedBy(T)"/>
        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }
    }
}
