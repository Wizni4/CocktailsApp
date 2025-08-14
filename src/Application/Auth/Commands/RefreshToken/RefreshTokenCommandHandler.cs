using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Auth
{
    public sealed class RefreshTokenCommandHandler(
        IAuthService authService,
        ITokenService tokenAccessor
    ) : ICommandHandler<RefreshTokenCommand, AuthTokens>
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenService _tokenAccessor = tokenAccessor;

        public async Task<AuthTokens> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (_tokenAccessor.RefreshToken == null) throw new UnauthorizedAccessException();
            return await _authService.RefreshTokenAsync(_tokenAccessor.RefreshToken, cancellationToken);
        }
    }
}
