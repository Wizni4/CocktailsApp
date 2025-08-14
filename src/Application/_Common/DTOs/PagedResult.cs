namespace CocktailsApp.Application.Common
{
    public sealed record PagedResult<T>(
        IReadOnlyCollection<T> Items,
        int Count,
        int? Next,
        int Total
    );
}
