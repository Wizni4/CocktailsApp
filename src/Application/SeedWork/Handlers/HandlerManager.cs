/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */


using Microsoft.Extensions.DependencyInjection;

namespace Application.SeedWork
{
    public class HandlerManager(IServiceProvider serviceProvider) : IHandlerManager
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        public IQueryHandler<TQuery, TDTO> Set<TQuery, TDTO>() where TQuery : IQuery
        {
            return _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TDTO>>();
        }

        public ICommandHandler<TCommand> Set<TCommand>() where TCommand : ICommand
        {
            return _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        }
    }
}
