/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using Application.Authentication.Commands;

using CocktailsApp.Application.SeedWork;

using MediatR;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Authentication
{
    public class SignUpCommandHandler(IAuthService authService) : ICommandHandler<SignUpCommand>
    {
        private readonly IAuthService _authService = authService;

        public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            await _authService.SignUpAsync(request);
        }
    }
}
