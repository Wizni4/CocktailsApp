using CocktailsApp.Application.Common;

namespace CocktailsApp.API.Common
{
    public static class IdempotencyKey
    {
        public static string HeaderName => "Idempotency-Key";
        public static string HttpItemKey => "IdempotencyKey";
    }

    public sealed class IdempotencyKeyService(
        IHttpContextAccessor httpContext
    ) : IIdempotencyKeyService
    {
        private readonly IHttpContextAccessor _httpContext = httpContext;
        public string? Value => _httpContext.HttpContext?.Request?.Headers[IdempotencyKey.HeaderName];
    }
}
