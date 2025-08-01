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
    public interface IAuthService : IService
    {
        Task<string> SignUpAsync(SignUpCommand request);
        Task<AuthDTO> SignInAsync(SignInCommand request);
        Task<AuthDTO> RefreshTokenAsync(string refreshToken);
        Task SignOutAsync(SignOutCommand request);
    }
}
