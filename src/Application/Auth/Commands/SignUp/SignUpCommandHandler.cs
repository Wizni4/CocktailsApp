using CocktailsApp.Application.Common;
using CocktailsApp.Application.Users;
using CocktailsApp.Domain.Users;

using MediatR;


namespace CocktailsApp.Application.Auth
{
    public sealed class SignUpCommandHandler(
        IAuthService authService,
        IUserRepository userRepository
    ) : ICommandHandler<SignUpCommand, Unit>
    {
        private readonly IAuthService _authService = authService;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var userId = await _authService.SignUpAsync(request, cancellationToken);

            var user = new UserFactory()
                .WithId(Guid.Parse(userId))
                .WithUsername(request.Username)
                .WithEmail(request.Email)
                .Build();

            await _userRepository.CreateAsync(user, cancellationToken);
            return Unit.Value;
        }
    }
}
