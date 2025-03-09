/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Shared
{
    public class Ingredient : ValueObject
    {
        public string Name { get; }
        internal Ingredient(string name)
        {
            Name = name;
        }
    }
}
