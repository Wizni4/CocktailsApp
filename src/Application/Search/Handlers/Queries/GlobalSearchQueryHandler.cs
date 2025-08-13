/*
*Domain namespaces
*/
/*
 * Application namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.Generic;
using System.Collections.ObjectModel;


/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Search
{
    public class GlobalSearchQueryHandler(
        IGlobalSearchService searchService
    ) : IQueryHandler<GlobalSearchQuery, ReadOnlyCollection<GlobalSearchResultDTO>>
    {
        private readonly IGlobalSearchService _searchService = searchService;
        public Task<ReadOnlyCollection<GlobalSearchResultDTO>> Handle(GlobalSearchQuery request, CancellationToken cancellationToken)
        {
            return _searchService.GetSearchResultsAsync(request, cancellationToken);
        }
    }
}
