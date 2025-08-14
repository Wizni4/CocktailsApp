using CocktailsApp.API.Common;

namespace CocktailsApp.API.Clubs
{
    public sealed record ClubMenuResponse(
        Guid ClubId,
        string ClubName,
        IReadOnlyCollection<MenuCocktailItemResponse> Cocktails
    ) : IResponse;

    public sealed record MenuCocktailItemResponse(
        Guid CocktailId,
        string Name,
        string? ImageUrl,
        bool ContainsAlcohol,
        IReadOnlyCollection<string> Allergens
    ) : IResponse;
}
