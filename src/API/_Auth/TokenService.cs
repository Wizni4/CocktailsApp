using CocktailsApp.Application.Auth;

namespace CocktailsApp.API.Auth
{
    public sealed class TokenService(
        ICookieService cookieService,
        IHttpContextAccessor httpContext
    ) : ITokenService
    {
        private readonly ICookieService _cookieService = cookieService;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public string? AccessToken
        {
            get
            {
                var h = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString();
                if (string.IsNullOrWhiteSpace(h)) return null;
                const string Bearer = "Bearer ";
                return h.StartsWith(Bearer, StringComparison.OrdinalIgnoreCase) ? h[Bearer.Length..] : null;
            }
        }

        public string? RefreshToken => _cookieService.GetRefreshTokenFromCookie();
    }
}
