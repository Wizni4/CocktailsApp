/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.Shared
{
    public class Ingredient : ValueObject
    {
        public string Name { get; private set; }
        public string Unit { get; private set; }

        internal Ingredient(string name, string unit)
        {
            Name = name;
            Unit = unit;
        }

        private protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Unit;
        }
    }
}
