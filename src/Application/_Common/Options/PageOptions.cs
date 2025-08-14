
namespace CocktailsApp.Application.Common
{
    public sealed record PageOptions(
        int DefaultLimit = 20,
        int MaxLimit = 100
    );
}
