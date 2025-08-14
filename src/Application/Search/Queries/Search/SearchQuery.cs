using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Search
{
    public record SearchQuery(
        string Term,
        int? Limit,
        int? Offset
    ) : IQuery<PagedResult<SearchResultItem>>;
}
