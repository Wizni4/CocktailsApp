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
        Task<string> SignUpAsync(SignUpCommand request);
        Task<SignInResponseDTO> SignInAsync(SignInCommand request);
        Task<SignInResponseDTO> RefreshTokenAsync(string refreshToken);
        Task SignOutAsync(SignOutCommand request);
    }
}
