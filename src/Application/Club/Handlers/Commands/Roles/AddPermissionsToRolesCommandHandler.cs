/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="AddPermissionsToRolesCommand"/> by delegating to the club aggregate 
    /// to grant a permission to a role, then returns the updated club as a DTO.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class AddPermissionsToRolesCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository,
        IMapper autoMapper
    ) : ICommandHandler<AddPermissionsToRolesCommand, IEnumerable<ClubRoleDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the permission assignment by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, target role ID, permission, and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public async Task<IEnumerable<ClubRoleDTO>> Handle(AddPermissionsToRolesCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.GetClubBydIdAsync(request.ClubId, opt => opt.Include(c => c.Roles));

            // Delegate the permission-adding logic to the club aggregate.
            foreach (var role in request.Roles)
                foreach (var permission in role.Permissions)
                    club!.AddPermissionToRole(role.Id, permission, request.ActorId);

            // Persist the changes.
            _clubRepository.Update(club!);
            await _unitOfWork.SaveChangesAsync();

            // Return the updated roles as a DTO.
            return _autoMapper.Map<IEnumerable<ClubRoleDTO>>(
                club!.Roles.Where(clubRoles => request.Roles.Any(updatedRoles => new ClubRoleByIdSpecification(updatedRoles.Id).SpecExpression.Compile()(clubRoles))));
        }
    }
}
