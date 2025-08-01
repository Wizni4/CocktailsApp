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
using CocktailsApp.Domain.ClubAggregate;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="UpdateRolesCommand"/> by deleting a role from a club.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class UpdateRolesCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository,
        IMapper autoMapper
    ) : ICommandHandler<UpdateRolesCommand, IEnumerable<ClubRoleDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the role deletion by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, role name and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public async Task<IEnumerable<ClubRoleDTO>> Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.GetClubBydIdAsync(request.ClubId, opt => opt.Include(c => c.Roles));

            // Update each specified roles
            foreach(var updatedRole in request.Roles)
            {
                // Update name if specified
                if (updatedRole.Name is not null)
                    club!.UpdateRoleName(updatedRole.Name, updatedRole.Id, request.ActorId);

                if (updatedRole.Permissions is not null)
                {
                    // Get the role form the club
                    var currentRole = club!.Roles.FirstOrDefault(new ClubRoleByIdSpecification(updatedRole.Id).SpecExpression.Compile());

                    // Get the Permisisons to add (= permissions not already associated to the role)
                    var permissionsToAdd = updatedRole.Permissions
                        .Where(newPermission => !currentRole!.Permissions.Any(p => new ClubPermissionByTypeSpecification(newPermission).SpecExpression.Compile()(p)));

                    // Add permissions if specified
                    if(permissionsToAdd.Any())
                        foreach (var addedPermission in permissionsToAdd)
                            club!.AddPermissionToRole(updatedRole.Id, addedPermission, request.ActorId);

                    // Get the Permisisons to remvoe (= permissions already associated to the role)
                    var permissionsToRemove = currentRole!.Permissions
                        .Where(p => !updatedRole.Permissions.Any(newPermission => new ClubPermissionByTypeSpecification(newPermission).SpecExpression.Compile()(p)))
                        .Select(p => p.Permission);

                    // Remove permissions if specified
                    if (permissionsToRemove.Any())
                        foreach (var removedPermission in permissionsToRemove)
                            club!.RemovePermissionFromRole(updatedRole.Id, removedPermission, request.ActorId);
                }     
            }

            // Persist the changes.
            _clubRepository.Update(club!);
            await _unitOfWork.SaveChangesAsync();

            // Return the updated roles as a DTO.
            return _autoMapper.Map<IEnumerable<ClubRoleDTO>>(
                club!.Roles.Where(clubRoles => request.Roles.Any(updatedRoles => new ClubRoleByIdSpecification(updatedRoles.Id).SpecExpression.Compile()(clubRoles))));
        }
    }
}
