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
        internal Ingredient(string name)
        {
            Name = name;
        }

        private protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
