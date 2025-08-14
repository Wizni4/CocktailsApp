using CocktailsApp.API.Common;

namespace CocktailsApp.API.Ingredients
{
    public sealed record IngredientDetailsResponse(
        Guid IngredientId,
        string Name,
        string Type,
        bool IsAlcoholic,
        string? ImageUrl,
        List<string> Allergens
    ) : IResponse;
}
