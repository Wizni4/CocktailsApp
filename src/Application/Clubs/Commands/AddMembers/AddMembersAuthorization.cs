using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    public sealed class AddMembersAuthorization(
        IClubAccess access
    ) : IAuthorize<AddMembersCommand>
    {
        private readonly IClubAccess _access = access;
        public async Task AuthorizeAsync(AddMembersCommand request, ICurrentUser user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            var caller = user.UserId;

            var allowed = await _access.IsOwner(request.ClubId, caller, cancellationToken)
                      || await _access.HasPermission(
                          request.ClubId,
                          caller,
                          [
                              PermissionType.AddMember,
                              PermissionType.AddRoleToMember
                          ],
                          cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
