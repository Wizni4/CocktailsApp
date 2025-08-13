/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using MediatR;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Authentication
{
    public record SignUpCommand(
        string Username,
        string Email,
        string Password
    ) : ICommand<Unit>;
}
