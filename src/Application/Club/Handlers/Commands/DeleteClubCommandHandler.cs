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
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="DeleteClubCommand"/> by deleting club.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class DeleteClubCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository,
        IMapper autoMapper
    ) : ICommandHandler<DeleteClubCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
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
        public async Task Handle(DeleteClubCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.GetClubBydIdAsync(request.ClubId);

            // Delegate the club removal to the domain
            club!.DeleteClub(request.ActorId);

            // Delete the club from the DB
            _clubRepository.Delete(club);

            // Persist the changes.
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
