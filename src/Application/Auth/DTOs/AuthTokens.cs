
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Auth
{
    public sealed record AuthTokens(
        string AccessToken,
        string? IdToken,
        string? RefreshToken
    ) : IDTO;
}
