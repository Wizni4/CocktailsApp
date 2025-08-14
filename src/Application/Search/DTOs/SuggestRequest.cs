

namespace CocktailsApp.Application.Search
{
    public sealed record SuggestRequest(
        string Prefix,
        AccessScope Scope,
        Guid? UserId,
        int Take = 5
    );
}
