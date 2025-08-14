using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Handles the <see cref="AddCocktailsCommand"/> by adding an existing <see cref="DomainCocktail"/> to a specified <see cref="Domain.Clubs.Club"/>,
    /// ensuring that the actor is authorized to perform the action, and returning the updated club as a DTO.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used to manage database operations.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public sealed class AddCocktailsCommandHandler(
        ICurrentUser user,
        IClubRepository clubRepository
    ) : ICommandHandler<AddCocktailsCommand, IEnumerable<Guid>>
    {
        private readonly ICurrentUser _user = user;
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
        public async Task<IEnumerable<Guid>> Handle(AddCocktailsCommand request, CancellationToken cancellationToken)
        {
            // Get the club from the database, including related entities.
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithCocktailsCommandSpecification(request.ClubId),
                cancellationToken);

            // Delegate addition logic to the domain layer.
            foreach (var cocktailId in request.CocktailIds)
                club!.AddCocktail(cocktailId, _user.UserId);

            // Persist changes to the database.
            await _clubRepository.UpdateAsync(club!, cancellationToken);

            // Return added club cocktails ids
            return club!.Cocktails.Select(c => c.Id);
        }
    }
}
