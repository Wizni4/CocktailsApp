/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class Cocktail : AggregateRoot, IAggregateRoot
    {
        public string Description { get => _description; }
        private string _description = null!;
        private readonly List<CocktailIngredient> _ingredients = [];
        public IReadOnlyCollection<CocktailIngredient> Ingredients { get { return _ingredients.AsReadOnly(); } }
        public string Name { get => _name; }
        private string _name = null!;

#pragma warning disable CS8618
        private Cocktail() { } // <----- EF forced me
#pragma warning restore CS8618

        internal Cocktail(string name, string description)
        {
            UpdateDescription(description);
            UpdateName(name);
        }

        public void AddIngredient(Ingredient ingredient, decimal quantity)
        {
            if (_ingredients.Any(ci => new CocktailIngredientByIngredientSpecification(ingredient).SpecExpression.Compile()(ci)))
                throw new ArgumentException($"Ingredient '{ingredient.Name}' is already in the cocktail.");

            var newCocktailIngredient = new CocktailIngredientBuilder()
                .WithIngredient(ingredient)
                .WithQuantity(quantity)
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

        public void RemoveIngredient(Ingredient ingredient)
        {
            var cocktailIngredient = GetIngredient(ingredient);
            _ingredients.Remove(cocktailIngredient);
            Touch();
        }

        public void UpdateIngredientQuantity(Ingredient ingredient, decimal newQuantity)
        {
            var cocktailIngredient = GetIngredient(ingredient);
            cocktailIngredient.UpdateQuantity(newQuantity);
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

        private CocktailIngredient GetIngredient(Ingredient ingredient)
        {
            var coctailIngredient = _ingredients.FirstOrDefault(ci => new CocktailIngredientByIngredientSpecification(ingredient).SpecExpression.Compile()(ci))
                ?? throw new ArgumentException($"Ingredient '{ingredient.Name}' is not part of the cocktail.");

            return coctailIngredient;
        }
    }
}
