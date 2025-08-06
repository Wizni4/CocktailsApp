/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;

/*
 * Application namespaces
 */


namespace CocktailsApp.Application.Club
{
    public class CreateRolesCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository
    ) : ICommandHandler<CreateRolesCommand, IEnumerable<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IClubRepository _clubRepository = clubRepository;

        public async Task<IEnumerable<Guid>> Handle(CreateRolesCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.GetClubBydIdAsync(request.ClubId, opt => opt.Include(c => c.Roles));

            // Create roles (delegated to the domain)
            foreach (var newRole in request.NewRoles)
            {
                var role = club!.CreateRole(newRole.Name, request.ActorId);

                // Add permissions if specified
                if (newRole.Permissions is not null)
                    foreach (var permission in newRole.Permissions)
                        club.AddPermissionToRole(role.Id, permission, request.ActorId);
            }

            // Persist the changes.
            _clubRepository.Update(club!);
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return club!.Roles.Select(x => x.Id);
        }
    }
}
