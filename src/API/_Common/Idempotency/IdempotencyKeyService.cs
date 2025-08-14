using CocktailsApp.Application.Common;

namespace CocktailsApp.API.Common
{
    public static class IdempotencyKey
    {
        public static string HeaderName => "Idempotency-Key";
        public static string HttpItemKey => "IdempotencyKey";
    }

    public sealed class IdempotencyKeyService(
        HttpContextAccessor httpContext
    ) : IIdempotencyKeyService
    {
        private readonly HttpContextAccessor _httpContext = httpContext;
        public string? Value => _httpContext.HttpContext?.Request?.Headers[IdempotencyKey.HeaderName];
    }
}
