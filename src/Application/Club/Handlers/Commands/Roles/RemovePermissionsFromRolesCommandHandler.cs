/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

/*
 * Application namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="RemovePermissionsFromRolesCommand"/> by removing a permission from a club role.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class RemovePermissionsFromRolesCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository,
        IMapper autoMapper
    ) : ICommandHandler<RemovePermissionsFromRolesCommand, IEnumerable<ClubRoleDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the permission removal by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, role name and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public async Task<IEnumerable<ClubRoleDTO>> Handle(RemovePermissionsFromRolesCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.GetClubBydIdAsync(request.ClubId, opt => opt.Include(c => c.Roles));

            // Delegate the permission-adding logic to the club aggregate.
            foreach (var role in request.Roles)
                foreach (var permission in role.Permissions)
                    club!.RemovePermissionFromRole(role.Id, permission, request.ActorId);

            // Persist the changes.
            _clubRepository.Update(club!);
            await _unitOfWork.SaveChangesAsync();

            // Return the updated roles as a DTO.
            return _autoMapper.Map<IEnumerable<ClubRoleDTO>>(
                club!.Roles.Where(clubRoles => request.Roles.Any(updatedRoles => new ClubRoleByIdSpecification(updatedRoles.Id).SpecExpression.Compile()(clubRoles))));
        }


    }
}
