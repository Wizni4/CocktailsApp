/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

namespace Application.SeedWork
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
