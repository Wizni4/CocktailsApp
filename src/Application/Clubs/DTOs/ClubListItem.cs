using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed record ClubListItem(
        Guid ClubId,
        string Name,
        string Description,
        string Visibility,
        string? City,
        string? Country,
        string? ImageId,
        int CocktailCount,
        int MemberCount,
        Guid OwnerUserId
     ) : Entity;
}
