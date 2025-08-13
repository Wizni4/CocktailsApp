/*
*Domain namespaces
*/
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Search
{
    public record GlobalSearchQuery(string Term, Guid UserId) : SearchQuery<ReadOnlyCollection<GlobalSearchResultDTO>>(Term);
}
