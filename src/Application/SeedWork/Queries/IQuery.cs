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
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
