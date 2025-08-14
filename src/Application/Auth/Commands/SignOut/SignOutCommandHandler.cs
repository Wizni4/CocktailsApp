using CocktailsApp.Application.Common;

using MediatR;


namespace CocktailsApp.Application.Auth
{
    public sealed class SignOutCommandHandler(
        ITokenService tokens,
        ICurrentUserService user,
        IAuthService authService
    ) : ICommandHandler<SignOutCommand, Unit>
    {
        private readonly ITokenService _tokens = tokens;
        private readonly ICurrentUserService _user = user;
        private readonly IAuthService _authService = authService;

        public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            var session = new AuthSession(
                AccessToken: _tokens.AccessToken,
                RefreshToken: _tokens.RefreshToken,
                UserId: _user.UserId,
                Username: _user.Username,
                SessionId: _user.SessionId
            );

            await _authService.SignOutAsync(session, cancellationToken);
            return Unit.Value;
        }
    }
}
