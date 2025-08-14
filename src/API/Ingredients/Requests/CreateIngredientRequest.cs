
using CocktailsApp.API.Common;

namespace CocktailsApp.API.Ingredients
{
    public sealed record CreateIngredientRequest(
        List<string>? Allergens,
        string Name,
        string Type,
        bool? IsAlcoholic
    ) : IRequest;
}
