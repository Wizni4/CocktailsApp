

using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Ingredients;

namespace CocktailsApp.Application.Ingredients
{
    public static class IngredientLoadGraph
    {
        public static readonly ILoad<Ingredient> Default = Load.For<Ingredient>("Default");
    }
}
