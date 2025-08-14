using MediatR;


namespace CocktailsApp.Application.Common
{
    public sealed class CachingBehavior<TRequest, TResponse>(
        ICacheService<TResponse> cache
    )
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableQuery
    {
        private readonly ICacheService<TResponse> _cache = cache;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var cached = await _cache.GetAsync(request.CacheKey, cancellationToken);
            if (cached is not null)
                return cached;

            var response = await next();
            await _cache.SetAsync(request.CacheKey, response!, request.Ttl, cancellationToken);
            return response;
        }
    }

}
