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
    public class SignOutCommandHandler(IAuthService authService) : ICommandHandler<SignOutCommand, Unit>
    {
        private readonly IAuthService _authService = authService;

        public async Task<Unit> Handle(SignOutCommand request, CancellationToken _)
        {
            await _authService.SignOutAsync(request);
            return Unit.Value;
        }
    }
}
