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
    public record SignUpCommand(string Login, string Password) : ICommand
    {
        public string Login { get; } = Login;
        public string Password { get; } = Password;
    }
}
