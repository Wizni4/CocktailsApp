using CocktailsApp.API.Authentication;
using CocktailsApp.Application.Common;

namespace CocktailsApp.API.Auth
{
    public sealed class TokenAccessor(
        ICookieService cookieService,
        IHttpContextAccessor httpContext
    ) : ITokenAccessor
    {
        private readonly ICookieService _cookieService = cookieService;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public string? AccessToken
        {
            get
            {
                var h = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString();
                if (string.IsNullOrWhiteSpace(h)) return null;
                const string bearer = "Bearer ";
                return h.StartsWith(bearer, StringComparison.OrdinalIgnoreCase) ? h[bearer.Length..] : null;
            }
        }

        public string? RefreshToken => _cookieService.GetRefreshTokenFromCookie();
    }
}
