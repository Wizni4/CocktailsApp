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
    public class SearchQueryHandler(
        ISearchService searchService
    ) : IQueryHandler<SearchQuery, IEnumerable<SearchResultDTO>>
    {
        private readonly ISearchService _searchService = searchService;
        public async Task<IEnumerable<SearchResultDTO>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            return await _searchService.GetSearchResultsAsync(request);
        }
    }
}
