
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    public sealed class RemoveCocktailsAuthorization(
        IClubAccess access
    ) : IAuthorize<RemoveCocktailsCommand>
    {
        private readonly IClubAccess _access = access;

        public async Task AuthorizeAsync(RemoveCocktailsCommand request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            var caller = user.UserId;

            var allowed = await _access.IsOwner(request.ClubId, caller, cancellationToken)
                      || await _access.HasPermission(
                          request.ClubId,
                          caller,
                          [PermissionType.RemoveCocktail],
                          cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
