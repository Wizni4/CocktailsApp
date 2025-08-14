

namespace CocktailsApp.Application.Auth
{
    public sealed record AuthSession(
        string? AccessToken,
        string? RefreshToken,
        Guid? UserId,         // from claims (local)
        string? Username,     // from claims (cognito "username"/"cognito:username")
        Guid? SessionId
    );
}
