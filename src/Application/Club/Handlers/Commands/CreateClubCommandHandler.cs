/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.Shared;
/*
 * Application namespaces
 */
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.Club
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
    public class CreateClubCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper autoMapper,
        IClubRepository clubRepository
    ) : ICommandHandler<CreateClubCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
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
            var club = new ClubBuilder()
                .WithAddress(_autoMapper.Map<Address>(request.Address))
                .WithDescription(request.Description)
                .WithName(request.Name)
                .WithOwner(request.OwnerId)
                .WithVisibility(request.Visibility)
                .Build();

            _clubRepository.Create(club);
            await _unitOfWork.SaveChangesAsync();
            return club.Id;
        }
    }
}
