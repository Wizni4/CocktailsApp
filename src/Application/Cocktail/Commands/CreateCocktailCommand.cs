/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using MediatR;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public class CreateCocktailCommand(string name, List<CocktailIngredientDTO> ingredients) : ICommand<CocktailDTO>
    {
        public string Name { get; } = name;
        public List<CocktailIngredientDTO> Ingredients { get; } = ingredients;
    }
}
