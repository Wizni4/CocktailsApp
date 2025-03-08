/*
 * Framework namespaces
 */
using System;
using System.Linq.Expressions;

namespace Domain.SeedWork
{
    /// <summary>
    /// Represents a composite specification that combines two other <see cref="Specification{T}"/> using a logical AND.<br/>
    /// 
    /// This class implements the specification pattern by combining two existing <see cref="Specification{T}"/> 
    /// and returning an expression that requires both specifications to be satisfied for an object to pass.<br/>
    /// 
    /// The <see cref="And{T}"/> class encapsulates the logic of combining two <see cref="Specification{T}"/> by using the <see cref="Expression.AndAlso(Expression, Expression)"/>
    /// operator to check if both the left and right specifications are satisfied by an object of type <typeparamref name="T"/>.<br/>
    ///
    /// This class is useful when you want to combine two conditions (or specifications) that must both be met in order to satisfy the overall condition.
    /// </summary>
    /// <param name="left">The left specification.</param>
    /// <param name="right">The right specification.</param>
    public class And<T>(
        ISpecification<T> left,
        ISpecification<T> right) : Specification<T>
    {
        readonly ISpecification<T> _left = left;
        readonly ISpecification<T> _right = right;

        /// <summary>
        /// Gets the expression that defines the combined specification using a logical AND between the left and right specifications.
        /// </summary>
        /// <value>
        /// An expression that represents the logical AND of the left and right specifications to be satisfied by an object of type <typeparamref name="T"/>.
        /// </value>
        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(
                        Expression.Invoke(_left.SpecExpression, objParam),
                        Expression.Invoke(_right.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
