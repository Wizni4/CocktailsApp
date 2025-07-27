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
    public record SignInCommand(string Login, string Password) : ICommand<SignInResponseDTO>
    {
        public string Login { get; } = Login;
        public string Password { get; } = Password;
    }
}
