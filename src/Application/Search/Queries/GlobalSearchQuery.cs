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
    public record GlobalSearchQuery(string Term, Guid UserId) : SearchQuery<IEnumerable<GlobalSearchResultDTO>>(Term);
}
