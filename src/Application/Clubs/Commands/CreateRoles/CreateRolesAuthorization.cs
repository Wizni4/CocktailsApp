using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

namespace CocktailsApp.Application.Clubs
{
    public sealed class CreateRolesAuthorization(
        IClubAccess access
    ) : IAuthorize<CreateRolesCommand>
    {
        private readonly IClubAccess _access = access;
        public async Task AuthorizeAsync(CreateRolesCommand request, ICurrentUser user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            var caller = user.UserId;

            var allowed = await _access.IsOwner(request.ClubId, caller, cancellationToken)
                      || await _access.HasPermission(
                          request.ClubId,
                          caller,
                          [
                              PermissionType.CreateRole,
                              PermissionType.AddPermissionToRole
                          ],
                          cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
