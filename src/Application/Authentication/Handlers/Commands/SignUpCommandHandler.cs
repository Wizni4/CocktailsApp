/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.User;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;

using MediatR;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Authentication
{
    public class SignUpCommandHandler(
        IAuthService authService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
    ) : ICommandHandler<SignUpCommand, Unit>
    {
        private readonly IAuthService _authService = authService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var userId = await _authService.SignUpAsync(request);

            var user = new UserBuilder()
                .WithId(Guid.Parse(userId))
                .WithUsername(request.Username)
                .WithEmail(request.Email)
                .Build();

            _userRepository.Create(user);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
