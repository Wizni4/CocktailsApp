namespace CocktailsApp.Application.Common
{
    public interface IIdempotencyService
    {
        Task<CacheHit<TResponse>> TryGetAsync<TResponse>(string key, CancellationToken ct);
        Task SaveAsync<TResponse>(string key, TResponse value, CancellationToken ct);
    }
}
