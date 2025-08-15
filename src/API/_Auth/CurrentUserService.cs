
using CocktailsApp.Application.Common;

using System.Security.Claims;

namespace CocktailsApp.API.Auth
{
    public sealed class CurrentUserService(
        IHttpContextAccessor httpContext
    ) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext = httpContext;
        public bool IsAuthenticated => _httpContext.HttpContext?.User?.Identity?.IsAuthenticated == true;

        public string? Username => GetUsername();

        public Guid UserId => GetUserId();

        public Guid? SessionId => GetSessionId();

        private Guid GetUserId()
        {
            var userIdClaim = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? _httpContext.HttpContext?.User.FindFirst("sub")?.Value
               ?? _httpContext.HttpContext?.User.FindFirst("userId")?.Value;

            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("User not authorized");

            return userId;
        }

        private string GetUsername()
        {
            var userName = _httpContext.HttpContext?.User.FindFirst("username")?.Value;

            if (userName is null)
                throw new UnauthorizedAccessException("User not authorized");

            return userName;
        }

        private Guid GetSessionId()
        {
            var sessionIdClaim = _httpContext.HttpContext?.User.FindFirst("sid")?.Value;

            if (sessionIdClaim is null || !Guid.TryParse(sessionIdClaim, out var sessionId))
                throw new UnauthorizedAccessException("User not authorized");

            return sessionId;
        }
    }
}
