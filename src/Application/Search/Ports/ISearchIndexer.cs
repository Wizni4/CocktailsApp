
namespace CocktailsApp.Application.Search
{
    public interface ISearchIndexer
    {
        Task UpsertAsync(SearchType type, Guid id, CancellationToken cancellationToken);
        Task RemoveAsync(SearchType type, Guid id, CancellationToken cancellationToken);
        Task RebuildAsync(SearchType? type, CancellationToken cancellationToken);
    }
}
