
namespace CocktailsApp.API.Common
{
    public sealed record PagedResponse<T>(
        IReadOnlyCollection<T> Items,
        int Count,
        int? Next,
        int Total
    );
}
