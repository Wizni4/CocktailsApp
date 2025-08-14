using CocktailsApp.Application.Common;

using MediatR;

namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Handles the <see cref="RemoveMembersCommand"/> by removing a member from a club.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public sealed class RemoveMembersCommandHandler(
        ICurrentUserService user,
        IClubRepository clubRepository
    ) : ICommandHandler<RemoveMembersCommand, Unit>
    {
        private readonly ICurrentUserService _user = user;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the member removal by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, role name and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public async Task<Unit> Handle(RemoveMembersCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithMembersCommandSpecification(request.ClubId),
                cancellationToken);

            // Delegate the removal to the domain
            foreach (var memberId in request.MemberIds)
                club!.RemoveMember(memberId, _user.UserId);

            // Update club
            await _clubRepository.UpdateAsync(club!, cancellationToken);

            // Return member removed successfully
            return Unit.Value;
        }
    }
}
