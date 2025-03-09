/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace Domain.SeedWork
{
    /// <summary>
    /// Represents a composite specification that negates the result of another <see cref="Specification{T}"/>.<br/>
    /// 
    /// This class implements the specification pattern by taking an existing <see cref="ISpecification{T}"/> 
    /// and returning an expression that inverts the result of the inner specification.<br/>
    /// If the inner specification is satisfied, the negated specification will not be satisfied, and vice versa.<br/>
    /// 
    /// The <see cref="Negated{T}"/> class encapsulates the logic of negating a specification by using the 
    /// <see cref="Expression.Not(Expression)"/> operator to negate the evaluation of the inner specification.<br/>
    /// 
    /// This class is useful when you want to specify the condition that an object must not satisfy a given specification.
    /// </summary>
    public class Negated<T>(ISpecification<T> inner) : Specification<T>
    {
        private readonly ISpecification<T> _inner = inner;

        /// <summary>
        /// Gets the expression that defines the negated specification.<br/>
        /// 
        /// The expression negates the result of the inner specification.<br/>
        /// 
        /// It uses the <see cref="Expression.Not(Expression)"/> operator to invert the evaluation of the inner specification's expression.
        /// </summary>
        /// <value>
        /// An expression that represents the negation of the inner specification's criteria.
        /// </value>
        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.Not(
                        Expression.Invoke(this._inner.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
