
namespace CocktailsApp.Application.Common
{
    public readonly struct CacheHit<T>(bool hasValue, T? value)
    {
        public bool HasValue { get; } = hasValue;
        public T? Value { get; } = value;
        public static CacheHit<T> Miss => new(false, default);
        public static CacheHit<T> Hit(T v) => new(true, v);
    }

    public interface ICacheService
    {
        Task<CacheHit<TResult>> GetAsync<TResult>(string key, CancellationToken cancellationToken);
        Task SetAsync<TResult>(string key, TResult value, TimeSpan ttl, CancellationToken cancellationToken);
    }
}
