using CocktailsApp.Domain.Users;

using MediatR;


namespace CocktailsApp.Application.Common
{
    public sealed class CachingBehavior<TRequest, TResponse>(
        ICurrentUserService user,
        ICacheService<TResponse> cache
    )
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableQuery
    {
        private readonly ICurrentUserService _user = user;
        private readonly ICacheService<TResponse> _cache = cache;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var cacheKey = request.CacheKey;

            // Scope to the caller when requested
            if (request is IActorScopedCache)
                cacheKey = $"{cacheKey}:{_user.UserId}";

            var cached = await _cache.GetAsync(cacheKey, cancellationToken);
            if (cached is not null)
                return cached;

            var response = await next();
            await _cache.SetAsync(cacheKey, response!, request.Ttl, cancellationToken);
            return response;
        }
    }

}
