/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

using MediatR;

namespace CocktailsApp.Application.SeedWork
{
    /// <summary>
    /// Defines a generic handler contract for processing commands or queries asynchronously.
    /// </summary>
    /// <typeparam name="TCommand">The type of command or query to handle.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the handler, which must be an <see cref="EntityDTO"/>.</typeparam>
    public interface IHandler<TCommand> : IRequestHandler<TCommand> where TCommand : IRequest
    {
    }
    /// <summary>
    /// Defines a generic handler contract for processing commands or queries asynchronously.
    /// </summary>
    /// <typeparam name="TCommand">The type of command or query to handle.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the handler, which must be an <see cref="EntityDTO"/>.</typeparam>
    public interface IHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
    }
}
