

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed record CocktailDetails(
        Guid CocktailId,
        string Name,
        string? Description,
        string? ImageId,
        IReadOnlyCollection<CocktailIngredientView> Ingredients
    ) : Entity;
    
    public sealed record CocktailIngredientView(
        Guid IngredientId,
        string IngredientName,
        bool IsAlcoholic,
        string IngredientType,
        decimal Quantity,
        string Unit,
        IReadOnlyCollection<string> Allergens
    ) : Entity;

}
