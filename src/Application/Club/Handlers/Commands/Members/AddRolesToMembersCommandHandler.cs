/*
 * Framework namespaces
 */
using AutoMapper;
using AutoMapper.Execution;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using MediatR;

/*
 * Application namespaces
 */

/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Handles the <see cref="AddRolesToMembersCommand"/> by delegating to the club aggregate 
    /// to grant a role to a member, then returns the updated club as a DTO.
    /// </summary>
    /// <param name="unitOfWork">The unit of work used for data access and persistence.</param>
    /// <param name="autoMapper">The AutoMapper instance used to map domain entities to DTOs.</param>
    public class AddRolesToMembersCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository
    ) : ICommandHandler<AddRolesToMembersCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IClubRepository _clubRepository = clubRepository;

        /// <summary>
        /// Handles the role assignment by loading the club aggregate,
        /// delegating the logic to the domain model, saving changes, and returning the result as a DTO.
        /// </summary>
        /// <param name="request">The command containing the club ID, target member Id, role ID and actor ID.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The updated <see cref="ClubDTO"/> reflecting the permission change.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified club does not exist.
        /// </exception>
        public async Task<Unit> Handle(AddRolesToMembersCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(new LoadClubWithMembersCommandSpecification(request.ClubId));

            // Add each user add their specified roles
            foreach (var member in request.Members)
                foreach (var roleId in member.RoleIds!)
                    club!.AddRoleToMember(member.Id, roleId, request.ActorId);

            // Persist the changes.
            _clubRepository.Update(club!);
            await _unitOfWork.SaveChangesAsync();

            // Return the updated members as a DTO.
            return Unit.Value;
        }
    }
}
