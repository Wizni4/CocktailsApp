using CocktailsApp.API.Common;


namespace CocktailsApp.API.Search
{
    public sealed record SearchItemResponse(
        string Type,
        Guid Id,
        string Title,
        string? Subtitle,
        string? ImageUrl,
        float Score,
        IReadOnlyDictionary<string, IReadOnlyList<string>>? Highlights
    ) : IResponse;
}
