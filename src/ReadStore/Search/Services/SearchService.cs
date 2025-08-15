
using CocktailsApp.Application.Common;
using CocktailsApp.Application.Search;

namespace CocktailsApp.ReadStore.Search
{
    public sealed class SearchService : ISearchService
    {
        public Task<PagedResult<SearchResultItem>> SearchAsync(SearchCriteria criteria, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<SearchResultItem>> SuggestAsync(SuggestRequest suggest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
