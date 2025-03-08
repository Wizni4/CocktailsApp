/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using Application.SeedWork;
/*
 * Framework namespaces
 */

namespace Application.Cocktails
{
    public interface ICocktailService : IService<CocktailDTO>
    {
        Task<IEnumerable<CocktailDTO>> GetCocktailsAsync();
    }
}
