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
                SetDescription(description);

            SetName(name);

            // raise Cocktail created event
            AddDomainEvent(new CocktailCreatedEvent(Id, Name, Description, ImageId, createdBy));
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

            // State that entity was updated
            Touch(createdBy);

            // Raise ingredient added event
            AddDomainEvent(new IngredientAddedEvent(
                Id,
                newCocktailIngredient.Id,
                newCocktailIngredient.IngredientId,
                newCocktailIngredient.Quantity,
                newCocktailIngredient.Unit,
                createdBy
            ));
        }

        public void DeleteCocktail(Guid actorId)
        {
            // Raise the event
            Touch(actorId);
            AddDomainEvent(new CocktailDeletedEvent(Id, actorId));
        }

        public void RemoveIngredient(Guid ingredientId, Guid actorId)
        {
            var cocktailIngredient = GetIngredient(ingredientId);
            _ingredients.Remove(cocktailIngredient);

            // State that entity was updated
            Touch(actorId);

            // Raise ingredient added event
            AddDomainEvent(new IngredientRemovedEvent(
                Id,
                cocktailIngredient.Id,
                actorId
            ));
        }

        public void UpdateIngredientQuantity(Guid ingredientId, decimal newQuantity, Guid actorId)
        {
            var cocktailIngredient = GetIngredient(ingredientId);
            cocktailIngredient.UpdateQuantity(newQuantity, actorId);

            // State that entity was updated
            Touch(actorId);

            // Raise ingredient added event
            AddDomainEvent(new IngredientQuantityChangedEvent(
                Id,
                cocktailIngredient.Id,
                cocktailIngredient.Quantity,
                actorId
            ));
        }

        public void UpdateIngredientUnit(Guid ingredientId, UnitOfMeasure newUnit, Guid actorId)
        {
            var cocktailIngredient = GetIngredient(ingredientId);
            cocktailIngredient.UpdateUnit(newUnit, actorId);

            // State that entity was updated
            Touch(actorId);

            // Raise ingredient added event
            AddDomainEvent(new IngredientUnitChangedEvent(
                Id,
                cocktailIngredient.Id,
                cocktailIngredient.Unit,
                actorId
            ));
        }

        public void UpdateName(string name, Guid actorId)
        {
            SetName(name);

            // State that entity was updated
            Touch(actorId);

            // Raise ingredient added event
            AddDomainEvent(new CocktailRenamedEvent(
                Id,
                Name,
                actorId
            ));
        }

        public void UpdateDescription(string description, Guid actorId)
        {
            SetDescription(description);

            // State that entity was updated
            Touch(actorId);

            // Raise ingredient added event
            AddDomainEvent(new CocktailDescriptionChangedEvent(
                Id,
                Description!,
                actorId
            ));
        }

        private CocktailIngredient GetIngredient(Guid ingredientId)
        {
            var coctailIngredient = _ingredients.FirstOrDefault(ci => new CocktailIngredientByIngredientSpecification(ingredientId).IsSatisfiedBy(ci))
                ?? throw new ArgumentException($"Ingredient '{ingredientId}' is not part of the cocktail.");

            return coctailIngredient;
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null");

            _description = description;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null");

            _name = name;
        }
    }
}
