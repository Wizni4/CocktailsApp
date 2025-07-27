/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

using CocktailsApp.Domain.SeedWork;

using MediatR;

namespace CocktailsApp.Application.SeedWork
{
    /// <summary>
    /// Represents a typed query against a domain entity.
    /// </summary>
    /// <typeparam name="TDomain">The type of the domain entity targeted by the query.</typeparam>
    public interface IQuery<TDomain, TResult> : IRequest<TResult>
    {
        // <summary>
        /// Gets the specification defining the query criteria.
        /// </summary>
        ISpecification<TDomain> Specification { get; }
        /// <summary>
        /// Gets an optional function describing how related entities should be included in the query result.
        /// </summary>
        Func<IIncludable<TDomain>, IIncludable>? Include { get; }
    }
}
