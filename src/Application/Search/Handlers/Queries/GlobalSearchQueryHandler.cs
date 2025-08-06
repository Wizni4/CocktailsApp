/*
*Domain namespaces
*/
/*
 * Application namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.Generic;


/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Search
{
    public class GlobalSearchQueryHandler(
        IGlobalSearchService searchService
    ) : IQueryHandler<GlobalSearchQuery, IEnumerable<GlobalSearchResultDTO>>
    {
        private readonly IGlobalSearchService _searchService = searchService;
        public Task<IEnumerable<GlobalSearchResultDTO>> Handle(GlobalSearchQuery request, CancellationToken cancellationToken)
        {
            return _searchService.GetSearchResultsAsync(request);
        }
    }
}
