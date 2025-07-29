/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;

namespace CocktailsApp.Application.Club
{
    public class AddMemberWithOptionsCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ClubCommandHandler<AddMemberWithOptionsCommand>(unitOfWork, autoMapper), ICommandHandler<AddMemberWithOptionsCommand, ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        public override async Task<ClubDTO> Handle(AddMemberWithOptionsCommand request, CancellationToken cancellationToken)
        {
            var club = await base.GetClubFromRepositoryAsync(request.ClubId);

            // Check if the user exists
            var user = await _unitOfWork.Set<Domain.UserAggregate.User>()
                .ReadAsync(new UserByIdSpecification(request.NewMemberUserId))
                ?? throw new KeyNotFoundException($"User with ID {request.NewMemberUserId} was not found.");

            // Add member to the club
            var member = club.AddMember(user.Id, request.ActorId);

            // Add optional data to the member:
            // -- Permissions
            if (request.Permissions is not null)
                club.AddPermissionsToMember(member.Id, request.Permissions, request.ActorId);

            // Add optional data to the member:
            // -- Roles
            if (request.RoleIds is not null)
                club.AddRolesToMember(member.Id, request.RoleIds, request.ActorId);

            // Persit data in DB
            await _unitOfWork.SaveChangesAsync();

            return _autoMapper.Map<ClubDTO>(club);
        }
    }
}
