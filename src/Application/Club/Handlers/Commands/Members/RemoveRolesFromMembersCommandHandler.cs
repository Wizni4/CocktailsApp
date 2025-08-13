/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using MediatR;

/*
 * Application namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="RemoveRolesFromMembersCommand"/> by removing a role from a club member.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class RemoveRolesFromMembersCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository
    ) : ICommandHandler<RemoveRolesFromMembersCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the role removal by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, role name and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public async Task<Unit> Handle(RemoveRolesFromMembersCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithMembersCommandSpecification(request.ClubId),
                cancellationToken);

            // Delegate the role-removal logic to the domain.
            foreach (var member in request.Members)
                foreach (var roleId in member.RoleIds)
                    club!.RemoveRoleFromMember(member.Id, roleId, request.ActorId);

            // Persist the changes.
            await _clubRepository.UpdateAsync(club!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return the updated members as a DTO.
            return Unit.Value;
        }


    }
}
