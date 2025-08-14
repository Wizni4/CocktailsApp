using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed class CreateRolesCommandHandler(
        ICurrentUserService user,
        IClubRepository clubRepository
    ) : ICommandHandler<CreateRolesCommand, IEnumerable<Guid>>
    {
        private readonly ICurrentUserService _user = user;
        private readonly IClubRepository _clubRepository = clubRepository;

        public async Task<IEnumerable<Guid>> Handle(CreateRolesCommand request, CancellationToken cancellationToken)
        {
            // Load the club aggregate, including roles.
            var club = await _clubRepository.ReadAsync(
                new LoadClubWithRolesCommandSpecification(request.ClubId),
                cancellationToken);

            // Create roles (delegated to the domain)
            foreach (var newRole in request.NewRoles)
            {
                var role = club!.CreateRole(newRole.Name, _user.UserId);

                // Add permissions if specified
                if (newRole.Permissions is not null)
                    foreach (var permission in newRole.Permissions)
                        club.AddPermissionToRole(role.Id, permission, _user.UserId);
            }

            // Update the club
            await _clubRepository.UpdateAsync(club!, cancellationToken);

            // Return all roles ids
            return club!.Roles.Select(x => x.Id);
        }
    }
}
