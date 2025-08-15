using CocktailsApp.Application.Common;
using CocktailsApp.Application.Ingredients;

namespace CocktailsApp.ReadStore.Ingredients
{
    public sealed class IngredientQueries : IIngredientQueries
    {
        public Task<PagedResult<IngredientDetails>> GetAllIngredientDetailsAsync(AllIngredientsCriteria criteria, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IngredientDetails?> GetIngredientDetailsAsync(Guid ingredientId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
