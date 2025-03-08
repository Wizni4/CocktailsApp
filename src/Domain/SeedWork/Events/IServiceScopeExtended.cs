/*
 * Framework namespaces
 */

namespace Domain.SeedWork
{
    public interface IServiceScopeExtended : IDisposable
    {
        IServiceProviderExtended ServiceProvider { get; }
    }
}
