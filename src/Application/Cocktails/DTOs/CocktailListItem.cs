

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed record CocktailListItem(
        Guid CocktailId,
        string Name,
        string? Description,
        string? imageId,
        bool ContainsAlcohol,
        List<string> Allergens,
        int IngredientCount
    ) : Entity;
}
