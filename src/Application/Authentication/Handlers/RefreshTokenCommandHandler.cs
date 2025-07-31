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
    public class RefreshTokenCommandHandler(IAuthService authService) : ICommandHandler<RefreshTokenCommand, AuthDTO>
    {
        private readonly IAuthService _authService = authService;

        public async Task<AuthDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RefreshTokenAsync(request.RefreshToken);
        }
    }
}
