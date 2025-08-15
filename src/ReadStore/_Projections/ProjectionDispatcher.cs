

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace CocktailsApp.ReadStore.Projections
{
    public class ProjectionDispatcher(
        IEventNameResolver resolver,
        IServiceProvider serviceProvider
    )
    {
        private readonly IEventNameResolver _resolver = resolver;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly JsonSerializerSettings _json = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        public async Task DispatchAsync(string typeName, string payload, CancellationToken ct)
        {
            var eventType = _resolver.Resolve(typeName)
                            ?? throw new InvalidOperationException($"Unknown event type '{typeName}'.");

            var @event = JsonConvert.DeserializeObject(payload, eventType, _json)
                         ?? throw new InvalidOperationException($"Cannot deserialize event '{typeName}'.");

            // Resolve all handlers registered for this T
            var handlerType = typeof(IProjectionHandler<>).MakeGenericType(eventType);
            var handlers = (IEnumerable<object>)_serviceProvider.GetServices(handlerType);

            foreach (var h in handlers)
            {
                var method = handlerType.GetMethod(nameof(IProjectionHandler<object>.HandleAsync))!;
                var task = (Task)method.Invoke(h, new[] { @event, ct })!;
                await task.ConfigureAwait(false);
            }
        }
    }
}
