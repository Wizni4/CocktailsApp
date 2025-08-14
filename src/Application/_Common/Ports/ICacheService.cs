
namespace CocktailsApp.Application.Common
{
    public interface ICacheService<TResult>
    {
        Task<TResult?> GetAsync(string key, CancellationToken cancellationToken);
        Task SetAsync(string key, TResult value, TimeSpan ttl, CancellationToken cancellationToken);
    }
}
