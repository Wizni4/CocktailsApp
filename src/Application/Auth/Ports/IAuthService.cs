using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Auth
{
    public interface IAuthService
    {
        Task<string> SignUpAsync(SignUpCommand request, CancellationToken cancellationToken);
        Task<AuthDTO> SignInAsync(SignInCommand request, CancellationToken cancellationToken);
        Task<AuthDTO> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task SignOutAsync(AuthSession session, CancellationToken cancellationToken);
    }
}
