using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Auth
{
    public sealed record SignInCommand(
        string Username,
        string Password
    ) : ICommand<AuthDTO>;
}
