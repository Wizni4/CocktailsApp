using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed class AddMembersCommandHandler(
        ICurrentUserService user,
        IClubRepository clubRepository
    ) : ICommandHandler<AddMembersCommand, IEnumerable<Guid>>
    {
        private readonly ICurrentUserService _user = user;
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
                var clubMember = club!.AddMember(newMember.UserId, _user.UserId);

                // Add roles to member
                if (newMember.RoleIds is not null)
                    foreach (var roleId in newMember.RoleIds)
                        club.AddRoleToMember(clubMember.Id, roleId, _user.UserId);
            }

            // Update the club
            await _clubRepository.UpdateAsync(club!, cancellationToken);

            // Return all members ids of the club
            return club!.Members.Select(m => m.Id);
        }
    }
}
