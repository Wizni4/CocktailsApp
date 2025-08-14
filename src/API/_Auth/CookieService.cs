
namespace CocktailsApp.API.Auth
{
    public class CookieService(IHttpContextAccessor httpContextAccessor) : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private const string RefreshCookieName = "refreshToken";

        private readonly CookieOptions _defaultCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        };

        public void SetRefreshTokenCookie(string? refreshToken)
        {
            if (refreshToken == null)
                throw new UnauthorizedAccessException("Unauthorized");

            var options = _defaultCookieOptions;
            options.Expires = DateTime.UtcNow.AddHours(8);
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(RefreshCookieName, refreshToken, options);
        }

        public void DeleteRefreshTokenCookie()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(RefreshCookieName, _defaultCookieOptions);
        }

        public string GetRefreshTokenFromCookie()
        {
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies[RefreshCookieName];

            if (refreshToken is null)
                throw new UnauthorizedAccessException("Unauthorized");

            return refreshToken;
        }
    }
}
