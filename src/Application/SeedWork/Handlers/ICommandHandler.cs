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
    /// Defines a handler for a command that returns a result.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <typeparam name="TResult">The type of the result returned by the handler.</typeparam>
    public interface ICommandHandler<TCommand, TResult> : IHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
    }
}
