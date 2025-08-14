using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed record UserClubs(
        Guid UserId,
        IReadOnlyCollection<UserClubItem> Clubs
    ) : Entity;

    public sealed record UserClubItem(
        Guid ClubId,
        string ClubName,
        bool IsOwner,
        IReadOnlyCollection<string> RoleNames,
        IReadOnlyCollection<string> EffectivePermissions
    ) : Entity;
}
