using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed record GetAllIngredientDetailsQuery(
        int? Limit,
        int? Offset
    ) : IQuery<PagedResult<IngredientDetails>>;
}
