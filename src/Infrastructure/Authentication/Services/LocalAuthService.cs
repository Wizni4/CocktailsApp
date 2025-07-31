using CocktailsApp.Application.Authentication;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.Authentication
{
    public class LocalAuthService(IJwtTokenGenerator jwtTokenGenerator) : IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        public Task<string> SignUpAsync(SignUpCommand request)
        {
            // You could simulate a duplicate check by throwing here if needed, or skip it
            return Task.FromResult(Guid.NewGuid().ToString());
        }

        public Task<AuthDTO> SignInAsync(SignInCommand command)
        {
            throw new NotImplementedException("Handled in Application layer");
        }

        public Task<AuthDTO> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(SignOutCommand command)
        {
            return Task.CompletedTask;
        }
    }
}
