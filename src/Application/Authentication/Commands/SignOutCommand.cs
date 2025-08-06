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
    public record SignOutCommand(string? Username) : ICommand<Unit>
    {
    }
}
