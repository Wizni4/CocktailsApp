using CocktailsApp.Application.Common;

using MediatR;

namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Handles the <see cref="RemovePermissionsFromRolesCommand"/> by removing a permission from a club role.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public sealed class RemovePermissionsFromRolesCommandHandler(
        ICurrentUserService user,
        IClubRepository clubRepository
    ) : ICommandHandler<RemovePermissionsFromRolesCommand, Unit>
    {
        private readonly ICurrentUserService _user = user;
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
        public async Task<Unit> Handle(RemovePermissionsFromRolesCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithRolesCommandSpecification(request.ClubId),
                cancellationToken);

            // Delegate the permission-adding logic to the club aggregate.
            foreach (var role in request.Roles)
                foreach (var permission in role.Permissions)
                    club!.RemovePermissionFromRole(role.Id, permission, _user.UserId);

            // Update club
            await _clubRepository.UpdateAsync(club!, cancellationToken);

            // Return permissions remove succesfully
            return Unit.Value;
        }


    }
}
