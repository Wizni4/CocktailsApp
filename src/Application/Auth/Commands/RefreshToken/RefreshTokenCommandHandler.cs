using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Auth
{
    public sealed class RefreshTokenCommandHandler(
        IAuthService authService,
        ITokenAccessor tokenAccessor
    ) : ICommandHandler<RefreshTokenCommand, AuthDTO>
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenAccessor _tokenAccessor = tokenAccessor;

        public async Task<AuthDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (_tokenAccessor.RefreshToken == null) throw new UnauthorizedAccessException();
            return await _authService.RefreshTokenAsync(_tokenAccessor.RefreshToken, cancellationToken);
        }
    }
}
