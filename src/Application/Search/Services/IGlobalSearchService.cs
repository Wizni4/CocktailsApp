/*
*Domain namespaces
*/
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Search
{
    public interface IGlobalSearchService : ISearchService<GlobalSearchResultDTO, GlobalSearchQuery>
    {
    }
}
