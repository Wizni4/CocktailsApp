

namespace CocktailsApp.Application.Auth
{
    public sealed record AuthSession(
        string? AccessToken,
        string? RefreshToken,
        Guid? UserId,
        string? Username
    );
}
