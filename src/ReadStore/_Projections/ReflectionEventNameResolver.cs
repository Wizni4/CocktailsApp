using System.Reflection;


namespace CocktailsApp.ReadStore.Projections
{
    public sealed class ReflectionEventNameResolver : IEventNameResolver
    {
        private readonly Dictionary<string, Type> _map;

        public ReflectionEventNameResolver(
            IEnumerable<Assembly> assemblies,
            string? namespacePrefix = null,
            Type? eventMarkerInterface = null)
        {
            _map = new(StringComparer.OrdinalIgnoreCase);

            foreach (var asm in assemblies.Distinct())
            {
                foreach (var t in asm.GetTypes())
                {
                    if (!t.IsClass || t.IsAbstract) continue;

                    // Optional filters
                    if (namespacePrefix is not null &&
                        (t.Namespace is null || !t.Namespace.StartsWith(namespacePrefix, StringComparison.Ordinal)))
                        continue;

                    if (eventMarkerInterface is not null && !eventMarkerInterface.IsAssignableFrom(t))
                        continue;

                    // Determine the event name
                    var defaultName = TrimSuffix(t.Name, "Event");
                    var keys = new List<string>
                    {
                        defaultName,
                        t.Name
                    };
                    if (t.FullName is not null) keys.Add(t.FullName);

                    foreach (var key in keys.Where(k => !string.IsNullOrWhiteSpace(k)))
                        AddKey(key!, t);
                }
            }
        }

        public Type? Resolve(string name) =>
            _map.TryGetValue(name, out var t) ? t : null;

        private void AddKey(string key, Type type)
        {
            if (_map.TryGetValue(key, out var existing) && existing != type)
                throw new InvalidOperationException(
                    $"Duplicate event name '{key}' for types '{existing.FullName}' and '{type.FullName}'. " +
                    "Add [EventName(\"UniqueName\")] to disambiguate.");
            _map[key] = type;
        }

        private static string TrimSuffix(string name, string suffix) =>
            name.EndsWith(suffix, StringComparison.Ordinal) ? name[..^suffix.Length] : name;
    }
}
