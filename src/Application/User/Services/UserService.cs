/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using MediatR;

/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.User
{
    public class UserService(IMediator mediator) : IUserService
    {
        private readonly IMediator _mediator = mediator;
        public Task<UserDTO> CreateUserAsync()
        {
            var command = new CreateUserCommand();
            return _mediator.Send(command);
        }
    }
}
