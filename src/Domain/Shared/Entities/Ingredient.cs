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
        public UnitOfMeasure BaseUnit { get; }
        internal Ingredient(string name, UnitOfMeasure baseUnit)
        {
            Name = name;
            BaseUnit = baseUnit;
        }
    }
}
