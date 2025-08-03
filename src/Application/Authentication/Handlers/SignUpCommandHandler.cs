/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Authentication
{
    public class SignUpCommandHandler(IAuthService authService, IUnitOfWork unitOfWork) : ICommandHandler<SignUpCommand>
    {
        private readonly IAuthService _authService = authService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var userId = await _authService.SignUpAsync(request);

            var user = new UserBuilder()
                .WithId(Guid.Parse(userId))
                .WithUsername(request.Username)
                .WithEmail(request.Email)
                .Build();

            _unitOfWork.Set<Domain.UserAggregate.User>().Create(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
