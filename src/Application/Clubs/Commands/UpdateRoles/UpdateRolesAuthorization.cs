
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

namespace CocktailsApp.Application.Clubs
{
    public sealed class UpdateRolesAuthorization(
        IClubAccess access
    ) : IAuthorize<UpdateRolesCommand>
    {
        private readonly IClubAccess _access = access;

        public async Task AuthorizeAsync(UpdateRolesCommand request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            var caller = user.UserId;

            var allowed = await _access.IsOwner(request.ClubId, caller, cancellationToken)
                       || await _access.HasPermission(
                          request.ClubId,
                          caller,
                          [
                              PermissionType.ChangeRoleName,
                              PermissionType.AddPermissionToRole,
                              PermissionType.RemovePermissionFromRole,
                          ],
                          cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
