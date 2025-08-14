using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Search
{
    public interface ISearchService
    {
        Task<PagedResult<SearchResultItem>> SearchAsync(SearchCriteria criteria, CancellationToken cancellationToken);
        Task<IReadOnlyList<SearchResultItem>> SuggestAsync(SuggestRequest suggest, CancellationToken cancellationToken);
    }
}
