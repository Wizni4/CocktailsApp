
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public interface IIngredientQueries
    {
        Task<IngredientDetails?> GetIngredientDetailsAsync(Guid ingredientId, CancellationToken cancellationToken);
        Task<PagedResult<IngredientDetails>> GetAllIngredientDetailsAsync(AllIngredientsCriteria criteria, CancellationToken cancellationToken);
    }
}
