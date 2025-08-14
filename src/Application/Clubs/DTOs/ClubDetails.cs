using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed record ClubDetails(
        Guid ClubId,
        string Name,
        string Description,
        string Visibility,
        string? Street,
        string? StreetNumber,
        string? City,
        string? PostalCode,
        string? State,
        string? Country,
        IReadOnlyCollection<ClubRoleView> Roles,
        IReadOnlyCollection<ClubMemberView> Members,
        IReadOnlyCollection<ClubCocktailView> Cocktails
    ) : Entity;

    public sealed record ClubRoleView(
        Guid RoleId,
        string Name,
        bool IsOwnerRole,
        IReadOnlyCollection<string> Permissions 
    ) : Entity;

    public sealed record ClubMemberView(
        Guid ClubMemberId,
        Guid UserId,
        string Username,
        List<string> Roles,
        bool IsOwner
    ) : Entity;

    public sealed record ClubCocktailView(
        Guid ClubCocktailId,
        Guid CocktailId,
        string CocktailName,
        string? ImageId
    ) : Entity;
}
