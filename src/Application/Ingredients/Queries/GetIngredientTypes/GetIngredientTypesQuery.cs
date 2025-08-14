
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Ingredients
{
    public sealed record GetIngredientTypesQuery : IQuery<IReadOnlyCollection<string>>;
}
