

using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Cocktails;

namespace CocktailsApp.Application.Cocktails
{
    public static class CocktailLoadGraph
    {
        public static readonly ILoad<Cocktail> Default = Load.For<Cocktail>("Default");
        public static readonly ILoad<Cocktail> Ingredients = Load.For<Cocktail>("Ingredients");
    }
}
