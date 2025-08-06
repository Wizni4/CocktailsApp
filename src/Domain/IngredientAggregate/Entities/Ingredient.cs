/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.IngredientAggregate
{
    public sealed class Ingredient : AggregateRoot
    {
        public IReadOnlyCollection<Allergen> Allergens { get => _allergens.AsReadOnly(); }
        private readonly List<Allergen> _allergens = [];
        public string Name { get => _name; }
        private readonly string _name = null!;
        public IngredientType Type { get => _type; }
        private readonly IngredientType _type;
        public bool IsAlcoholic { get => _isAlcoholic; }
        private readonly bool _isAlcoholic;
        private Ingredient() { }
        internal Ingredient(
            string? name,
            IngredientType type,
            bool isAlcoholic,
            Guid createdBy
        ) : base(createdBy)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Ingredient name cannot be null or empty");

            _name = name;
            _type = type;
            _isAlcoholic = isAlcoholic;
        }

        public Allergen AddAllergen(string name)
        {
            var allergen = new Allergen(name);

            if (_allergens.Contains(allergen))
                throw new ArgumentException($"Ingredient '{Name}' already contains the allergen '{name}'");

            _allergens.Add(allergen);
            Touch();
            return allergen;
        }

        public void RemoveAllergen(string name)
        {
            var allergen = new Allergen(name);

            if (!_allergens.Contains(allergen))
                throw new ArgumentException($"Ingredient '{Name}' does not contain the allergen '{name}'");

            _allergens.Remove(allergen);
            Touch();
        }
    }
}
