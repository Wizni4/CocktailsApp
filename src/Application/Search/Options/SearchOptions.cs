

namespace CocktailsApp.Application.Search
{
    public sealed record SearchOptions(
        int DefaultLimit = 20,
        int MaxLimit = 100,
        bool UseFuzziness = true,
        string Fuzziness = "AUTO",
        bool EnableHighlights = true,
        int SuggestLimit = 5
    );
}
