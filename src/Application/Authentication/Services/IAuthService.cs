/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using Application.Authentication.Commands;

using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Authentication
{
    public interface IAuthService
    {
        Task SignUpAsync(SignUpCommand request);
        Task<SignInResponseDTO> SignInAsync(SignInCommand request);
        Task SignOutAsync(SignOutCommand request);
    }
}
