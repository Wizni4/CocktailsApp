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
    public interface ICommand : IBaseRequest;
    /// <summary>
    /// Marker interface representing a command in the application.
    /// Commands typically represent actions or intents to change state.
    /// </summary>
    public interface ICommand<TResult> : ICommand, IRequest<TResult>;

}
