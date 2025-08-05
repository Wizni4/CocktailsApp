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
    public class SignOutCommandHandler(IAuthService authService) : ICommandHandler<SignOutCommand>
    {
        private readonly IAuthService _authService = authService;

        public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            await _authService.SignOutAsync(request);
        }
    }
}
