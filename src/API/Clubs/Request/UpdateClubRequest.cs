using CocktailsApp.API.Common;

namespace CocktailsApp.API.Clubs
{
    public sealed record UpdateClubRequest(
        Guid ClubId,
        AddressRequest? Address,
        string? Name,
        string? Description,
        string? Visibility
    ) : IRequest;
}
