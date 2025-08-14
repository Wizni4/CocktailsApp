
using CocktailsApp.Application.Common;

using MediatR;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Handles the <see cref="DeleteRolesCommand"/> by deleting a role from a club.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public sealed class DeleteRolesCommandHandler(
        ICurrentUser user,
        IClubRepository clubRepository
    ) : ICommandHandler<DeleteRolesCommand, Unit>
    {
        private readonly ICurrentUser _user = user;
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
        public async Task<Unit> Handle(DeleteRolesCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithRolesCommandSpecification(request.ClubId),
                cancellationToken);

            // Delete specified roles
            foreach (var roleId in request.RoleIds)
                club!.DeleteRole(roleId, _user.UserId);

            // Update club
            await _clubRepository.UpdateAsync(club!, cancellationToken);

            // Return role deleted successfuly
            return Unit.Value;
        }
    }
}
