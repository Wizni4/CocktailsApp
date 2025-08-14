using CocktailsApp.API.Common;
using CocktailsApp.Domain.Common;

namespace CocktailsApp.API.Cocktails
{
    public sealed record CreateCocktailRequest(
        string Name,
        string? Description,
        List<CocktailIngredientRequest> Ingredients
    ) : IRequest;

    public sealed record CocktailIngredientRequest(
        Guid Id,
        decimal Quantity,
        string Unit
    ) : IRequest;
}
