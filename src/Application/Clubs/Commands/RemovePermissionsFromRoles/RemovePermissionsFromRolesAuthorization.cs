

using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

namespace CocktailsApp.Application.Clubs
{
    public sealed class RemovePermissionsFromRolesAuthorization(
        IClubAccess access    
    ) : IAuthorize<RemovePermissionsFromRolesCommand>
    {
        private readonly IClubAccess _access = access;

        public async Task AuthorizeAsync(RemovePermissionsFromRolesCommand request, ICurrentUser user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            var caller = user.UserId;

            var allowed = await _access.IsOwner(request.ClubId, caller, cancellationToken)
                       || await _access.HasPermission(
                          request.ClubId,
                          caller,
                          [PermissionType.RemovePermissionFromRole],
                          cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
