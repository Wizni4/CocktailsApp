using CocktailsApp.Application.Auth;
using CocktailsApp.Infrastructure.Common;

namespace CocktailsApp.Infrastructure.Auth
{
    public class LocalAuthService(IJwtTokenGenerator jwtTokenGenerator) : IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        public Task<AuthTokens> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AuthTokens> SignInAsync(SignInCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(AuthSession session, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> SignUpAsync(SignUpCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
