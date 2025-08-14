using CocktailsApp.API.Common;

namespace CocktailsApp.API.Identity
{
    public sealed record SignUpRequest(
        string Username,
        string Email,
        string Password
    ) : IRequest;
}
