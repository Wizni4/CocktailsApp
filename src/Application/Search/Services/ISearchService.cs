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
    public interface ISearchService : IService
    {
        Task<IEnumerable<SearchResultDTO>> GetSearchResultsAsync(SearchQuery tequeryrm);
    }
}
