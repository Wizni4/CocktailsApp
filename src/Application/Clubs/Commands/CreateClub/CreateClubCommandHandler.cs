using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Handles the <see cref="CreateClubCommand"/> by creating a new club and returning its data as a <see cref="ClubDTO"/>.
    /// </summary>
    /// <remarks>
    /// This handler constructs the club aggregate using the builder pattern, persists it using the unit of work,
    /// and maps the result to a DTO for return.
    /// </remarks>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public sealed class CreateClubCommandHandler(
        ICurrentUserService user,
        IClubRepository clubRepository
    ) : ICommandHandler<CreateClubCommand, Guid>
    {
        private readonly ICurrentUserService _user = user;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the club creation command by building and saving a new club entity.
        /// </summary>
        /// <param name="request">The request containing the information needed to create a club.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="ClubDTO"/> containing the created club's data.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when the ownerId from the <paramref name="request"/> is not found in the db.
        /// </exception>
        public async Task<Guid> Handle(CreateClubCommand request, CancellationToken cancellationToken)
        {
            var clubBuilder = new ClubFactory()
                .WithAddress(
                    request.Address.Street,
                    request.Address.StreetNumber,
                    request.Address.City,
                    request.Address.PostalCode,
                    request.Address.State,
                    request.Address.Country
                )
                .WithDescription(request.Description)
                .WithName(request.Name)
                .WithOwner(_user.UserId);

            if (request.Visibility != null)
                clubBuilder.WithVisibility((Visibility)request.Visibility);


            var club = clubBuilder.Build();
            await _clubRepository.CreateAsync(club, cancellationToken);
            return club.Id;
        }
    }
}
