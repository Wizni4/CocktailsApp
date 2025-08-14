namespace CocktailsApp.Application.Ingredients
{
    public sealed record class AllIngredientsCriteria(
        int Skip = 0,
        int Take = 20
    );
}
