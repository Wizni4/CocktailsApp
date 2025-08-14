
namespace CocktailsApp.Application.Search
{
    public sealed record SearchCriteria(
        string Term,
        AccessScope Scope,
        Guid? UserId,
        int Skip = 0,
        int Take = 20,
        bool WithHighlights = true
    );
}
