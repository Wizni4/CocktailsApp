/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public sealed class Cocktail : AggregateRoot, IAggregateRoot
    {
        public string? Description { get => _description; }
        private string? _description = null;
        private readonly List<CocktailIngredient> _ingredients = [];
        public IReadOnlyCollection<CocktailIngredient> Ingredients { get { return _ingredients.AsReadOnly(); } }
        public string Name { get => _name; }
        private string _name = null!;
        private Cocktail() { }
        internal Cocktail(string name, string? description, Guid createdBy) : base(createdBy)
        {
            if (description != null)
                UpdateDescription(description);

            UpdateName(name);
        }

        public void AddIngredient(Guid ingredientId, decimal quantity, UnitOfMeasure unit, Guid createdBy)
        {
            if (_ingredients.Any(ci => new CocktailIngredientByIngredientSpecification(ingredientId).IsSatisfiedBy(ci)))
                throw new ArgumentException($"Ingredient '{ingredientId}' is already in the cocktail.");

            var newCocktailIngredient = new CocktailIngredientBuilder()
                .WithCreatorId(createdBy)
                .WithIngredient(ingredientId)
                .WithQuantity(quantity)
                .WithUnit(unit)
                .Build();

            _ingredients.Add(newCocktailIngredient);
            Touch();
        }

        public void DeleteCocktail()
        {
            // Raise the event
            AddDomainEvent(new CocktailDeletedEvent(Id));
            Touch();
        }

        public void RemoveIngredient(Guid ingredientId)
        {
            var cocktailIngredient = GetIngredient(ingredientId);
            _ingredients.Remove(cocktailIngredient);
            Touch();
        }

        public void UpdateIngredientQuantity(Guid ingredientId, decimal newQuantity)
        {
            var cocktailIngredient = GetIngredient(ingredientId);
            cocktailIngredient.UpdateQuantity(newQuantity);
            Touch();
        }

        public void UpdateIngredientUnit(Guid ingredientId, UnitOfMeasure newUnit)
        {
            var cocktailIngredient = GetIngredient(ingredientId);
            cocktailIngredient.UpdateUnit(newUnit);
            Touch();
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null");

            _name = name;
            Touch();
        }

        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null");

            _description = description;
            Touch();
        }

        private CocktailIngredient GetIngredient(Guid ingredientId)
        {
            var coctailIngredient = _ingredients.FirstOrDefault(ci => new CocktailIngredientByIngredientSpecification(ingredientId).IsSatisfiedBy(ci))
                ?? throw new ArgumentException($"Ingredient '{ingredientId}' is not part of the cocktail.");

            return coctailIngredient;
        }
    }
}
