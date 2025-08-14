namespace CocktailsApp.Application.Common
{
    public sealed record PagedResult<T>(IReadOnlyList<T> Items, int Total);
}
