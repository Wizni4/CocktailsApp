using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    public sealed class AddCocktailsAuthorization(
        IClubAccess access
    ) : IAuthorize<AddCocktailsCommand>
    {
        private readonly IClubAccess _access = access;

        public async Task AuthorizeAsync(AddCocktailsCommand request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            var caller = user.UserId;

            var allowed = await _access.IsOwner(request.ClubId, caller, cancellationToken)
                      || await _access.HasPermission(
                          request.ClubId,
                          caller,
                          [PermissionType.AddCocktail],
                          cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
