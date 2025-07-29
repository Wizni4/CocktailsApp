/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.Club
{
    public class CreateRoleWithOptionsCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ClubCommandHandler<CreateRoleWithOptionsCommand>(unitOfWork, autoMapper), ICommandHandler<CreateRoleWithOptionsCommand, ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;


        public override async Task<ClubDTO> Handle(CreateRoleWithOptionsCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await base.GetClubFromRepositoryAsync(request.ClubId);

            // Delegate the role-adding logic to the club aggregate.
            var role  = club.CreateRole(request.RoleName, request.Permissions, request.ActorId);

            // Persist the changes.
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return _autoMapper.Map<ClubDTO>(club);
        }
    }
}
