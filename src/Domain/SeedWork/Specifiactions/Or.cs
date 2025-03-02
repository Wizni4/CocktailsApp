/*
 * Framework namespaces
 */
using System;
using System.Linq.Expressions;

namespace Domain.SeedWork
{
    /// <summary>
    /// Represents a composite specification that combines two other specifications using a logical OR.<br/>
    /// 
    /// This class implements the specification pattern by combining two existing <see cref="ISpecification{T}"/> instances.<br/>
    /// 
    /// The resulting specification will be satisfied if either the left or the right specification is satisfied for an object of type <typeparamref name="T"/>.<br/>
    /// 
    /// The <see cref="Or{T}"/> class encapsulates the logic of combining two specifications by using the 
    /// <see cref="Expression.OrElse(Expression, Expression)"/> operator to check if either the left or right 
    /// specification is satisfied by an object.<br/>
    /// 
    /// This class is useful when you want to combine two conditions (or specifications) where one or the other must be met in order to satisfy the overall condition.
    /// </summary>
    public class Or<T>(
        ISpecification<T> left,
        ISpecification<T> right) : SpecificationBase<T>
    {
        readonly ISpecification<T> left = left;
        readonly ISpecification<T> right = right;

        /// <summary>
        /// Gets the expression that defines the combined specification using a logical OR between the left and right specifications.<br/>
        /// 
        /// The expression uses the <see cref="Expression.OrElse(Expression, Expression)"/> operator to combine the two specifications.<br/>
        /// 
        /// The result will be true if either the left or right specification is satisfied.
        /// </summary>
        /// <value>
        /// An expression that represents the logical OR of the left and right specifications.
        /// </value>
        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(
                        Expression.Invoke(left.SpecExpression, objParam),
                        Expression.Invoke(right.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
