/*
 * Framework namespaces
 */
using Microsoft.Extensions.DependencyInjection;

namespace Domain.SeedWork
{
    /// <summary>
    /// http://www.udidahan.com/2009/06/14/domain-events-salvation/
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate>? s_actions;

        private static IServiceProvider? s_serviceProvider;

        public static void Init(IServiceProvider serviceProvider)
        {
            s_serviceProvider = serviceProvider;
        }

        //Registers a callback for the given domain event, used for testing only
        public static void Register<T>(Action<T> callback) where T : DomainEvent
        {
            ArgumentNullException.ThrowIfNull(callback);

            s_actions ??= [];

            s_actions.Add(callback);
        }

        //Clears callbacks passed to Register on the current thread
        public static void ClearCallbacks()
        {
            s_actions = null;
        }

        //Raises the given domain event
        public static void Raise<T>(T args) where T : DomainEvent
        {
            ArgumentNullException.ThrowIfNull(args);

            if (s_serviceProvider != null)
            {
                var eventHandle = s_serviceProvider.GetRequiredService<IHandler<T>>();
                eventHandle.Handle(args);
            }

            if (s_actions != null)
                foreach (var action in s_actions)
                    if (action is Action<T> action1)
                        action1(args);
        }
    }
}
