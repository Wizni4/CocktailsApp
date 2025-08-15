using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed record UserClubs(
        Guid UserId,
        IReadOnlyCollection<UserClubItem> Clubs
    ) : IDTO;

    public sealed record UserClubItem(
        Guid ClubId,
        string ClubName,
        string ClubDescription,
        bool IsOwner,
        string? ImageId,
        IReadOnlyCollection<string> RoleNames,
        IReadOnlyCollection<string> EffectivePermissions
    ) : IDTO;
}
