
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

namespace CocktailsApp.Application.Clubs
{
    public sealed class RemoveMembersAuthorization(
        IClubAccess access
    ) : IAuthorize<RemoveMembersCommand>
    {
        private readonly IClubAccess _access = access;
        public async Task AuthorizeAsync(RemoveMembersCommand request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            var caller = user.UserId;

            var allowed = await _access.IsOwner(request.ClubId, caller, cancellationToken)
                       || await _access.HasPermission(
                          request.ClubId,
                          caller,
                          [PermissionType.RemoveMember],
                          cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
