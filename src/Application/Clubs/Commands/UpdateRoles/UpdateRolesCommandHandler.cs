using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

using MediatR;

namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Handles the <see cref="UpdateRolesCommand"/> by deleting a role from a club.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public sealed class UpdateRolesCommandHandler(
        ICurrentUserService user,
        IClubRepository clubRepository
    ) : ICommandHandler<UpdateRolesCommand, Unit>
    {
        private readonly ICurrentUserService _user = user;
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
        public async Task<Unit> Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithRolesCommandSpecification(request.ClubId),
                cancellationToken);

            // Update each specified roles
            foreach (var updatedRole in request.Roles)
            {
                // Update name if specified
                if (updatedRole.Name is not null)
                    club!.UpdateRoleName(updatedRole.Name, updatedRole.Id, _user.UserId);

                if (updatedRole.Permissions is not null)
                {
                    // Get the role form the club
                    var currentRole = club!.Roles.FirstOrDefault(new ClubRoleByIdSpecification(updatedRole.Id).SpecExpression.Compile());

                    // Get the Permisisons to add (= permissions not already associated to the role)
                    var permissionsToAdd = updatedRole.Permissions
                        .Where(newPermission => !currentRole!.Permissions.Any(p => new ClubPermissionByTypeSpecification(newPermission).SpecExpression.Compile()(p)));

                    // Add permissions if specified
                    if (permissionsToAdd.Any())
                        foreach (var addedPermission in permissionsToAdd)
                            club!.AddPermissionToRole(updatedRole.Id, addedPermission, _user.UserId);

                    // Get the Permisisons to remvoe (= permissions already associated to the role)
                    var permissionsToRemove = currentRole!.Permissions
                        .Where(p => !updatedRole.Permissions.Any(newPermission => new ClubPermissionByTypeSpecification(newPermission).SpecExpression.Compile()(p)))
                        .Select(p => p.Permission);

                    // Remove permissions if specified
                    if (permissionsToRemove.Any())
                        foreach (var removedPermission in permissionsToRemove)
                            club!.RemovePermissionFromRole(updatedRole.Id, removedPermission, _user.UserId);
                }
            }

            // Persist the changes.
            await _clubRepository.UpdateAsync(club!, cancellationToken);

            // Return the updated roles as a DTO.
            return Unit.Value;
        }
    }
}
