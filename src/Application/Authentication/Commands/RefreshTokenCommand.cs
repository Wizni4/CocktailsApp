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
    public record RefreshTokenCommand(string RefreshToken) : ICommand<AuthDTO>
    {
    }
}
