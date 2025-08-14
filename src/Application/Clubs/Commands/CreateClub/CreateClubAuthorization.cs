using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed class CreateClubAuthorization(
        IClubAccess access
    ) : IAuthorize<CreateClubCommand>
    {
        private readonly IClubAccess _access = access;
        public async Task AuthorizeAsync(CreateClubCommand request, ICurrentUserService user, CancellationToken cancellationToken)
        {
            if (!user.IsAuthenticated) throw new UnauthorizedAccessException();
            var caller = user.UserId;

            var allowed = await _access.CanCreateClub(caller, cancellationToken);

            if (!allowed) throw new UnauthorizedAccessException();
        }
    }
}
