using CocktailsApp.API.Common;

namespace CocktailsApp.API.Identity
{
    public sealed record SignInResponse(
        string AccessToken,
        string? IdToken,
        string? RefreshToken
    ) : IResponse;
}
