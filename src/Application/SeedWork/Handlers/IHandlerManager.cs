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
    public interface IHandlerManager
    {
        IQueryHandler<TQuery, TDTO> Set<TQuery, TDTO>() where TQuery : IQuery;
        ICommandHandler<TCommand> Set<TCommand>() where TCommand : ICommand;
    }
}
