/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="UpdateDescriptionCommand"/> by deleting a role from a club.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class UpdateDescriptionCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ClubCommandHandler<UpdateDescriptionCommand>(unitOfWork, autoMapper), ICommandHandler<UpdateDescriptionCommand, ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        /// <summary>
        /// Handles the role deletion by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, role name and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public override async Task<ClubDTO> Handle(UpdateDescriptionCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await base.GetClubFromRepositoryAsync(request.ClubId);

            // Delegate the removal to the domain
            club.UpdateDescription(request.NewDescription, request.ActorId);

            // Persist the changes.
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return _autoMapper.Map<ClubDTO>(club);
        }

        
    }
}
