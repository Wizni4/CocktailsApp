

namespace CocktailsApp.ReadStore.Projections
{
    public interface IEventNameResolver
    {
        Type? Resolve(string eventTypeName);
    }
}
