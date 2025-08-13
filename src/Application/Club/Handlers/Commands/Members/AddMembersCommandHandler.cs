/*
 * Framework namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Application namespaces
 */
/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class AddMembersCommandHandler(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository
    ) : ICommandHandler<AddMembersCommand, IEnumerable<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IClubRepository _clubRepository = clubRepository;

        public async Task<IEnumerable<Guid>> Handle(AddMembersCommand request, CancellationToken cancellationToken)
        {
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithMembersCommandSpecification(request.ClubId),
                cancellationToken);

            // Add members to the club
            foreach (var newMember in request.NewMembers)
            {
                // Add member
                var clubMember = club!.AddMember(newMember.UserId, request.ActorId);

                // Add roles to member
                if (newMember.RoleIds is not null)
                    foreach (var roleId in newMember.RoleIds)
                        club.AddRoleToMember(clubMember.Id, roleId, request.ActorId);
            }

            // Persit data in DB
            await _clubRepository.UpdateAsync(club!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return club!.Members.Select(m => m.Id);
        }
    }
}
