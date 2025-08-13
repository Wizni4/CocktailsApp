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
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var userId = await _authService.SignUpAsync(request);

            var user = new UserBuilder()
                .WithId(Guid.Parse(userId))
                .WithUsername(request.Username)
                .WithEmail(request.Email)
                .Build();

            await _userRepository.CreateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
