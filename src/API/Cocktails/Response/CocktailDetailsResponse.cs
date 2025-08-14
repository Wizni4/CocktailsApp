using CocktailsApp.API.Common;

namespace CocktailsApp.API.Cocktails
{
    public sealed record CocktailDetailsResponse(
        Guid CocktailId,
        string Name,
        string? Description,
        string? ImageUrl,
        IReadOnlyCollection<CocktailIngredientResponse> Ingredients
    ) : IResponse;

    public sealed record CocktailIngredientResponse(
        Guid IngredientId,
        string IngredientName,
        bool IsAlcoholic,
        string IngredientType,
        decimal Quantity,
        string Unit,
        string? ImageUrl,
        IReadOnlyCollection<string> Allergens
    ) : IResponse;
}
