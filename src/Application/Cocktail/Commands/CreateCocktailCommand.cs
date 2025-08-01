/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public record CreateCocktailCommand(
        string Descripotion,
        string Name,
        List<IngredientDTO> Ingredients
   ) : ICommand<CocktailDTO>;
}
