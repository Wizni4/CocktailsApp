using CocktailsApp.Application.Search;

namespace CocktailsApp.Application.Common
{
    public interface ISearchService
    {
        Task<PagedResult<SearchResultItem>> SearchAsync(SearchCriteria criteria, CancellationToken cancellationToken);
        Task<IReadOnlyList<SearchResultItem>> SuggestAsync(SuggestRequest suggest, CancellationToken cancellationToken);
    }
}
