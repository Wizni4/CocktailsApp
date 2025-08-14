using CocktailsApp.Application.Common;
using Microsoft.Extensions.Options;


namespace CocktailsApp.Application.Search
{
    public class SearchQueryHandler(
        ICurrentUser user,
        IOptions<SearchOptions> options,
        ISearchService searchService
    ) : IQueryHandler<SearchQuery, PagedResult<SearchResultItem>>
    {
        private readonly ICurrentUser _user = user;
        private readonly IOptions<SearchOptions> _options = options;
        private readonly ISearchService _searchService = searchService;

        public Task<PagedResult<SearchResultItem>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var cfg = _options.Value;
            var take = Math.Clamp(request.Limit ?? cfg.DefaultLimit, 1, cfg.MaxLimit);
            var skip = Math.Max(request.Offset ?? 0, 0);

            var scope = _user.IsAuthenticated ? AccessScope.PublicOrMember : AccessScope.PublicOnly;

            var criteria = new SearchCriteria(
                Term          : request.Term.Trim(),
                Scope         : scope,
                UserId        : _user.UserId,
                Skip          : skip,
                Take          : take,
                WithHighlights: cfg.EnableHighlights
            );

            return _searchService.SearchAsync(criteria, cancellationToken);
        }
    }
}
