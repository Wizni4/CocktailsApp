
using MediatR;

namespace CocktailsApp.Application.Common
{
    public interface ICommand : IBaseRequest;
    /// <summary>
    /// Marker interface representing a command in the application.
    /// Commands typically represent actions or intents to change state.
    /// </summary>
    public interface ICommand<TResult> : ICommand, IRequest<TResult>;

}
