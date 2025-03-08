/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using Application.SeedWork;
using Application.Shared;
/*
 * Framework namespaces
 */

namespace Application.Cocktails
{
    public interface ICocktailService : IService<CocktailDTO>
    {
        Task<CocktailDTO> GetCocktailByIdAsync(Guid cocktailId);
        Task<IEnumerable<CocktailDTO>> GetAllCocktailsAsync();
        Task<IEnumerable<CocktailDTO>> GetCocktailsByNameAsync(string name);
        Task<IEnumerable<CocktailDTO>> GetCocktailsByIngredientsAsync(List<IngredientDTO> ingredient);
        Task CreateCocktailAsync(string name, IEnumerable<CocktailIngredientDTO> ingredients);
    }
}
