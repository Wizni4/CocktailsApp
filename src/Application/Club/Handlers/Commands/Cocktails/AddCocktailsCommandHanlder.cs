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
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="AddCocktailsCommand"/> by adding an existing <see cref="Domain.CocktailAggregate.Cocktail"/> to a specified <see cref="Domain.ClubAggregate.Club"/>,
    /// ensuring that the actor is authorized to perform the action, and returning the updated club as a DTO.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used to manage database operations.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class AddCocktailCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ClubCommandHandler<AddCocktailsCommand>(unitOfWork, autoMapper), ICommandHandler<AddCocktailsCommand, ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        /// <summary>
        /// Handles the command to add a cocktail to a club.
        /// </summary>
        /// <param name="request">The command containing the club ID, cocktail ID, and actor ID.</param>
        /// <param name="cancellationToken">A cancellation token for the async operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> after the cocktail has been added.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if either the specified club or cocktail could not be found.
        /// </exception>
        public override async Task<ClubDTO> Handle(AddCocktailsCommand request, CancellationToken cancellationToken)
        {
            // Get the club from the database, including related entities.
            var club = await base.GetClubFromRepositoryAsync(request.ClubId, c => c.Include(c => c.Cocktails));

            // Get the cocktails to be added.
            var cocktails = await _unitOfWork.Set<Domain.CocktailAggregate.Cocktail>()
                .ReadRangeAsync(new CocktailByIdsSpecification(request.CocktailIds.ToList()));

            // Get the list of missing cocktails
            var missingCocktailIds = request.CocktailIds.Where(cocktailId => !cocktails.Any(c => c.Id == cocktailId));

            // If some specified cocktails ID does not exist:
            // -> Throw an exception with all missing Ids
            if (missingCocktailIds.Any())
                throw new KeyNotFoundException($"The following cocktails IDs were not found:\n-{string.Join("\n- ", missingCocktailIds)}");

            // Delegate domain logic to the aggregate.
            club.AddCocktails(cocktails.Select(c => c.Id), request.ActorId);

            // Persist changes to the database.
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return _autoMapper.Map<ClubDTO>(club);
        }
    }
}
