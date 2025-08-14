
namespace CocktailsApp.Application.Search
{
    public sealed record SearchResultItem(
        SearchType Type,
        Guid Id,
        string Title,
        string? Subtitle,
        string? ImageId,
        float Score,
        IReadOnlyDictionary<string, IReadOnlyList<string>>? Highlights
    );
}
