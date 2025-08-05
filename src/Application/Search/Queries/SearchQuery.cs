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
    public record SearchQuery(string Term, Guid UserId) : IQuery<IEnumerable<SearchResultDTO>>;
}
