
namespace CocktailsApp.Application.Common
{
    public sealed class PageOptions
    {
        public int DefaultLimit { get; } = 20;
        public int MaxLimit { get; } = 100;
    }
}
