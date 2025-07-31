/*
 * Framework namespaces
 */
using AutoMapper;

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="AddCocktailsCommand"/> by adding an existing <see cref="DomainCocktail"/> to a specified <see cref="Domain.ClubAggregate.Club"/>,
    /// ensuring that the actor is authorized to perform the action, and returning the updated club as a DTO.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used to manage database operations.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class AddCocktailCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository,
        IMapper autoMapper
    ) : ICommandHandler<AddCocktailsCommand, IEnumerable<ClubCocktailDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the command to add a cocktail to a club.
        /// </summary>
        /// <param name="request">The command containing the club ID, cocktail ID, and actor ID.</param>
        /// <param name="cancellationToken">A cancellation token for the async operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> after the cocktail has been added.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if either the specified club or cocktail could not be found.
        /// </exception>
        public async Task<IEnumerable<ClubCocktailDTO>> Handle(AddCocktailsCommand request, CancellationToken cancellationToken)
        {
            // Get the club from the database, including related entities.
            var club = await _clubRepository.GetClubBydIdAsync(request.ClubId, c => c.Include(c => c.Cocktails));

            // Delegate addition logic to the domain layer.
            foreach(var cocktailId in request.CocktailIds)
                club!.AddCocktail(cocktailId, request.ActorId);

            // Persist changes to the database.
            _clubRepository.Update(club!);
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return _autoMapper.Map<IEnumerable<ClubCocktailDTO>>(club!.Cocktails);
        }
    }
}
