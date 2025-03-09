/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.CocktailAggregate
{
    public class Cocktail : Entity, IAggregateRoot
    {
        private readonly List<CocktailIngredient> _ingredients = [];
        public IReadOnlyCollection<CocktailIngredient> Ingredients { get { return _ingredients.AsReadOnly(); } }
        public string Name { get; private set; }

        internal Cocktail(string name, List<CocktailIngredient> ingredients)
        {
            Name = name;
            _ingredients = ingredients;
        }
    }
}
