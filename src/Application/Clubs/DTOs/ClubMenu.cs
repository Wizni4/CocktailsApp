using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed record ClubMenu(
        Guid ClubId,
        string ClubName,
        IReadOnlyCollection<MenuCocktailItem> Cocktails
    ) : Entity;

    public sealed record MenuCocktailItem(
        Guid CocktailId,
        string Name,
        string? ImageId,
        bool ContainsAlcohol,
        IReadOnlyCollection<string> Allergens
    ) : Entity;
}
