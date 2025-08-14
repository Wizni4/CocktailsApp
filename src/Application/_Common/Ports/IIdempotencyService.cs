namespace CocktailsApp.Application.Common
{
    public interface IIdempotencyService
    {
        Task<(bool Found, TResponse? Value)> TryGetAsync<TResponse>(string key, CancellationToken ct);
        Task SaveAsync<TResponse>(string key, TResponse value, CancellationToken ct);
    }
}
