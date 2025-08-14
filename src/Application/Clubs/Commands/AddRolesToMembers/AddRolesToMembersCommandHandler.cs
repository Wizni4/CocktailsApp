
using CocktailsApp.Application.Common;

using MediatR;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Handles the <see cref="AddRolesToMembersCommand"/> by delegating to the club aggregate 
    /// to grant a role to a member, then returns the updated club as a DTO.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public sealed class AddRolesToMembersCommandHandler(
        ICurrentUserService user,
        IClubRepository clubRepository
    ) : ICommandHandler<AddRolesToMembersCommand, Unit>
    {
        private readonly ICurrentUserService _user = user;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the role assignment by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, target member Id, role ID and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public async Task<Unit> Handle(AddRolesToMembersCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithMembersRolesCommandSpecification(request.ClubId),
                cancellationToken);

            // Add, to each member, their specified roles
            foreach (var member in request.Members)
                foreach (var roleId in member.RoleIds!)
                    club!.AddRoleToMember(member.Id, roleId, _user.UserId);

            // Update the club
            await _clubRepository.UpdateAsync(club!, cancellationToken);

            // Return member added successfully
            return Unit.Value;
        }
    }
}
