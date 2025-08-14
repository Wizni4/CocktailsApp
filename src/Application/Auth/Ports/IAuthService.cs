using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Auth
{
    public interface IAuthService
    {
        Task<string> SignUpAsync(SignUpCommand request, CancellationToken cancellationToken);
        Task<AuthTokens> SignInAsync(SignInCommand request, CancellationToken cancellationToken);
        Task<AuthTokens> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task SignOutAsync(AuthSession session, CancellationToken cancellationToken);
    }
}
