/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

using System.Security;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Ingredients
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

            if (!Enum.IsDefined(type))
                throw new ArgumentException("Type is invalid.");

            _name = name;
            _type = type;
            _isAlcoholic = isAlcoholic;

            // raise ingredient created event
            AddDomainEvent(new IngredientCreatedEvent(Id, Name, Type, IsAlcoholic, CreatedBy));
        }

        public Allergen AddAllergen(string name, Guid actorId)
        {
            var allergen = new Allergen(name);

            if (_allergens.Contains(allergen))
                throw new ArgumentException($"Ingredient '{Name}' already contains the allergen '{name}'");

            _allergens.Add(allergen);

            // State that entity changed
            Touch(actorId);

            // raise allergen added event
            AddDomainEvent(new AllergenAddedEvent(Id, allergen.Name, actorId));

            return allergen;
        }

        public void DeleteIngredient(Guid actorId)
        {
            // State that entity changed
            Touch(actorId);

            // raise allergen added event
            AddDomainEvent(new IngredientDeletedEvent(Id, actorId));
        }

        public void RemoveAllergen(string name, Guid actorId)
        {
            var allergen = new Allergen(name);

            if (!_allergens.Contains(allergen))
                throw new ArgumentException($"Ingredient '{Name}' does not contain the allergen '{name}'");

            _allergens.Remove(allergen);

            // State that entity changed
            Touch(actorId);

            // raise allergen added event
            AddDomainEvent(new AllergenRemovedEvent(Id, allergen.Name, actorId));
        }
    }
}
