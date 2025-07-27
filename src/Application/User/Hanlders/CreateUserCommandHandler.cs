/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;

namespace CocktailsApp.Application.User
{
    public class CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ICommandHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserBuilder().Build();
            _unitOfWork.Set<Domain.UserAggregate.User>().Create(user);
            await _unitOfWork.SaveChangesAsync();
            return _autoMapper.Map<UserDTO>(user);
        }
    }
}
