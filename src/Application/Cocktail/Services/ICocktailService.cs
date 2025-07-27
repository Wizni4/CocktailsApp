/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public interface ICocktailService : IService<CocktailDTO>
    {
        Task<CocktailDTO> GetCocktailByIdAsync(Guid cocktailId);
        Task<IEnumerable<CocktailDTO>> GetAllCocktailsAsync();
        Task<IEnumerable<CocktailDTO>> GetCocktailsByNameAsync(string name);
        Task<IEnumerable<CocktailDTO>> GetCocktailsByIngredientsAsync(List<IngredientDTO> ingredient);
        Task<CocktailDTO> CreateCocktailAsync(string name, IEnumerable<CocktailIngredientDTO> ingredients);
    }
}
