using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Auth
{
    public sealed class SignInCommandHandler(
        IAuthService authService
    ) : ICommandHandler<SignInCommand, AuthDTO>
    {
        private readonly IAuthService _authService = authService;
        public async Task<AuthDTO> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            return await _authService.SignInAsync(request, cancellationToken);
        }
    }
}
