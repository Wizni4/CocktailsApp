/*
 * Framework namespaces
 */

namespace Domain.SeedWork
{
    public interface IServiceProviderExtended
    {
        IServiceScopeExtended CreateScope();
        T GetRequiredService<T>() where T : notnull;
    }
}
