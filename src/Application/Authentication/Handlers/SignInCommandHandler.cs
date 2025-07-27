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
    public class SignInCommandHandler(IAuthService authService) : ICommandHandler<SignInCommand, SignInResponseDTO>
    {
        private readonly IAuthService _authService = authService;
        public async Task<SignInResponseDTO> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            return await _authService.SignInAsync(request);
        }
    }
}
