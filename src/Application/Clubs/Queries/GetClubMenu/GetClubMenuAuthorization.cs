using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed class GetClubMenuAuthorization(
        IClubAccess access
    ) : IAuthorize<GetClubMenuQuery>
    {
        private readonly IClubAccess _access = access;

        public async Task AuthorizeAsync(GetClubMenuQuery request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();

            var allowed = await _access.CanViewClub(request.ClubId, user.UserId, cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
