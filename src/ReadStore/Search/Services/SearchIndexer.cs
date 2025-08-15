

using CocktailsApp.Application.Search;

namespace CocktailsApp.ReadStore.Search
{
    public sealed class SearchIndexer : ISearchIndexer
    {
        public Task RebuildAsync(SearchType? type, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(SearchType type, Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpsertAsync(SearchType type, Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
