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
using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="AddPermissionToMemberCommand"/> by delegating to the club aggregate 
    /// to grant a permission to a member, then returns the updated club as a DTO.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class AddPermissionToMemberCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ClubCommandHandler<AddPermissionToMemberCommand>(unitOfWork, autoMapper), ICommandHandler<AddPermissionToMemberCommand, ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        /// <summary>
        /// Handles the permission assignment by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, target member ID, permission, and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public override async Task<ClubDTO> Handle(AddPermissionToMemberCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including members.
            var club = await base.GetClubFromRepositoryAsync(request.ClubId);

            // Delegate the permission-adding logic to the club aggregate.
            club.AddPermissionToMember(request.MemberId, request.Permission, request.ActorId);

            // Persist the changes.
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return _autoMapper.Map<ClubDTO>(club);
        }
    }
}
