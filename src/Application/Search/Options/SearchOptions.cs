

namespace CocktailsApp.Application.Search
{
    public sealed class SearchOptions
    {
        public int DefaultLimit { get; } = 20;
        public int MaxLimit { get; } = 100;
        public bool UseFuzziness { get; } = true;
        public string Fuzziness { get; } = "AUTO";
        public bool EnableHighlights { get; } = true;
        public int SuggestLimit { get; } = 5;
    }
}
