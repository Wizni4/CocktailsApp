
using CocktailsApp.API.Common;

namespace CocktailsApp.API.Identity
{
    public sealed record SignInRequest(
        string Username,
        string Password
    ) : IRequest;
}
