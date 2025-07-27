using Application.Authentication.Commands;

using CocktailsApp.Application.Authentication;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.Authentication
{
    public class LocalAuthService(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork) : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        public async Task SignUpAsync(SignUpCommand command)
        {
            if (await this.IsUserExist(command.Login))
                throw new Exception("User already exists.");

            var user = new UserBuilder()
                .WithLogin(command.Login)
                .WithPassword(command.Password)
                .Build();

             _unitOfWork.Set<User>().Create(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<SignInResponseDTO> SignInAsync(SignInCommand command)
        {
            var user = await _unitOfWork.Set<User>().ReadAsync(new UserByLoginSpecification(command.Login))
                ?? throw new ArgumentException("User not found");


            if (user.Password != command.Password)
                throw new UnauthorizedAccessException("Invalid login or password.");

            var token = _jwtTokenGenerator.GenerateToken(user.Id);

            return new SignInResponseDTO { Token = token };
        }

        public Task SignOutAsync(SignOutCommand command)
        {
            // For stateless JWT, signout can be no-op or blacklist token in real apps
            return Task.CompletedTask;
        }

        private async Task<bool> IsUserExist(string login)
        {
            return (await _unitOfWork.Set<User>().ReadAsync(new UserByLoginSpecification(login))) is not null;
        }
    }
}
