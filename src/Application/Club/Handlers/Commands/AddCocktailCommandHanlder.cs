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
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="AddCocktailCommand"/> by adding an existing <see cref="Domain.CocktailAggregate.Cocktail"/> to a specified <see cref="Domain.ClubAggregate.Club"/>,
    /// ensuring that the actor is authorized to perform the action, and returning the updated club as a DTO.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used to manage database operations.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class AddCocktailCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ClubCommandHandler<AddCocktailCommand>(unitOfWork, autoMapper), ICommandHandler<AddCocktailCommand, ClubDTO>
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
        public override async Task<ClubDTO> Handle(AddCocktailCommand request, CancellationToken cancellationToken)
        {
            // Get the club from the database, including related entities.
            var club = await base.GetClubFromRepositoryAsync(request.ClubId, c => c.Include(c => c.Cocktails));

            // Get the cocktail to be added.
            var cocktail = await _unitOfWork.Set<Domain.CocktailAggregate.Cocktail>()
                .ReadAsync(new CocktailByIdSpecification(request.CocktailId))
                ?? throw new KeyNotFoundException($"Cocktail with ID {request.CocktailId} was not found.");

            // Delegate domain logic to the aggregate.
            club.AddCocktail(cocktail.Id, request.ActorId);

            // Persist changes to the database.
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return _autoMapper.Map<ClubDTO>(club);
        }
    }
}
