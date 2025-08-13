/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Authentication
{
    public record SignInCommand(
        string Username,
        string Password
    ) : ICommand<AuthDTO>;
}
