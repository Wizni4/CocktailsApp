using CocktailsApp.Application.Common;

using MediatR;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Handles the <see cref="DeleteClubCommand"/> by deleting club.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public sealed class DeleteClubCommandHandler(
        ICurrentUserService user,
        IClubRepository clubRepository
    ) : ICommandHandler<DeleteClubCommand, Unit>
    {
        private readonly ICurrentUserService _user = user;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the club deletion by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, role name and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public async Task<Unit> Handle(DeleteClubCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(
                new LoadClubCommandSpecification(request.ClubId),
                cancellationToken);

            // Delegate the club removal to the domain
            club!.DeleteClub(_user.UserId);

            // Delete the club
            await _clubRepository.DeleteAsync(club, cancellationToken);

            return Unit.Value;
        }
    }
}
