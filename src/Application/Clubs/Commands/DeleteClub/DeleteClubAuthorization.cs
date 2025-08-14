
using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed class DeleteClubAuthorization(
        IClubAccess access
    ) : IAuthorize<DeleteClubCommand>
    {
        private readonly IClubAccess _access = access;
        public async Task AuthorizeAsync(DeleteClubCommand request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            var caller = user.UserId;

            var allowed = await _access.IsOwner(request.ClubId, caller, cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
