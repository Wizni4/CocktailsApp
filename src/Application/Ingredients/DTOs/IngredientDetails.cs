

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed record IngredientDetails(
        Guid IngredientId,
        string Name,
        string Type,
        bool IsAlcoholic,
        string? ImageId,
        List<string> Allergens
    ) : IDTO;
}
