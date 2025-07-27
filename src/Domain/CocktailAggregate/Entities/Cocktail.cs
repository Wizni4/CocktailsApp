/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class Cocktail : Entity, IAggregateRoot
    {
        private readonly List<CocktailIngredient> _ingredients = [];
        public IReadOnlyCollection<CocktailIngredient> Ingredients { get { return _ingredients.AsReadOnly(); } }
        public string Name { get; private set; }

#pragma warning disable CS8618
        private Cocktail() { } // <----- EF forced me
#pragma warning restore CS8618

        internal Cocktail(string name, List<CocktailIngredient> ingredients)
        {
            Name = name;
            _ingredients = ingredients;
        }
    }
}
