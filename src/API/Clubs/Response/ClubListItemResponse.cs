
using CocktailsApp.API.Common;

namespace CocktailsApp.API.Clubs
{
    public sealed record ClubListItemResponse(
        Guid ClubId,
        string Name,
        string Description,
        string Visibility,
        string? City,
        string? Country,
        string? ImageUrl,
        int CocktailCount,
        int MemberCount,
        Guid OwnerUserId
    ) : IResponse;
}
