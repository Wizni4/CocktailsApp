namespace CocktailsApp.Application.Common
{
    public interface ICacheableQuery : IQuery
    {
        string CacheKey { get; }
        TimeSpan Ttl { get; }
    }
}
