
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Auth
{
    public sealed record AuthDTO(
        string AccessToken,
        string? IdToken,
        string? RefreshToken
    ) : Entity;
}
