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
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="AddMemberCommand"/> by adding a new user as a member to a specified club,
    /// validating both the club and user exist, and returning the updated club as a DTO.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used to access and persist domain entities.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class AddMemberCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ClubCommandHandler<AddMemberCommand>(unitOfWork, autoMapper), ICommandHandler<AddMemberCommand, ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        /// <summary>
        /// Handles the command to add a new member to a club.
        /// </summary>
        /// <param name="request">The command containing the club ID, the new user's ID, and the actor performing the action.</param>
        /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> after the new member has been added.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club or user could not be found.
        /// </exception>
        public override async Task<ClubDTO> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            // Get the club from the database, including related entities.
            var club = await base.GetClubFromRepositoryAsync(request.ClubId);

            // Check if the user exists
            var user = await _unitOfWork.Set<Domain.UserAggregate.User>()
                .ReadAsync(new UserByIdSpecification(request.NewMemberUserId))
                ?? throw new KeyNotFoundException($"User with ID {request.NewMemberUserId} was not found.");

            // Delegate the member-adding logic to the club aggregate.
            club.AddMember(user.Id, request.ActorId);

            // Persist the changes.
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return _autoMapper.Map<ClubDTO>(club);
        }
    }

}
