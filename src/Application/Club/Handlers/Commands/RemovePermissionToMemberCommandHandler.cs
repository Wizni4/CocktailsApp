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

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="RemovePermissionToMemberCommand"/> by permission from a club member.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class RemovePermissionToMemberCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ClubCommandHandler<RemovePermissionToMemberCommand>(unitOfWork, autoMapper), ICommandHandler<RemovePermissionToMemberCommand, ClubDTO>
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
        public override async Task<ClubDTO> Handle(RemovePermissionToMemberCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await base.GetClubFromRepositoryAsync(request.ClubId);

            // Delegate the removal to the domain
            club.RemovePermissionToMember(request.MemberId, request.Permission, request.ActorId);

            // Persist the changes.
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return _autoMapper.Map<ClubDTO>(club);
        }

        
    }
}
