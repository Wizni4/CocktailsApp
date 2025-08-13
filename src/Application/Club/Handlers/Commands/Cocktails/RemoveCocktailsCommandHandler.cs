/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.Cocktail;

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using MediatR;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="RemoveCocktailsCommand"/> by removing a cocktail from a club.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class RemoveCocktailsCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository
    ) : ICommandHandler<RemoveCocktailsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the cocktail removal by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, role name and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public async Task<Unit> Handle(RemoveCocktailsCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithCocktailsCommandSpecification(request.ClubId),
                cancellationToken);

            // Delegate the removal to the domain
            foreach (var cocktailId in request.CocktailIds)
                club!.RemoveCocktail(cocktailId, request.ClubId);

            // Persist the changes.
            await _clubRepository.UpdateAsync(club!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return the updated club as a DTO.
            return Unit.Value;
        }
    }
}
