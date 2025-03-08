/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using Application.SeedWork;

/*
 * Framework namespaces
 */

namespace Application.Cocktails
{
    public class CreateCocktailCommand(string name, List<CocktailIngredientDTO> ingredients) : ICommand
    {
        public string Name { get; } = name;
        public List<CocktailIngredientDTO> Ingredients { get; } = ingredients;
    }
}
