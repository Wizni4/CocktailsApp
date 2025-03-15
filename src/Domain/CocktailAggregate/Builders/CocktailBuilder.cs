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
    public class CocktailBuilder : IBuilder<Cocktail>
    {
        private readonly List<CocktailIngredient> _ingredients = [];
        private string _name = "";

        public CocktailBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }

        public CocktailBuilder AddIngredient(CocktailIngredient ingredient)
        {
            if (!_ingredients.Any(i => i == ingredient))
                _ingredients.Add(ingredient);
            return this;
        }

        public CocktailBuilder AddIngredient(Ingredient ingredient, decimal quantity)
        {
            if (_ingredients.Any(ci => ci.Ingredient == ingredient))
                throw new ArgumentException("Ingredient already exists in the cocktail.", nameof(ingredient));

            var cocktailIngredient = new CocktailIngredientBuilder()
                .WithIngredient(ingredient)
                .WithQuantity(quantity)
                .Build();

            _ingredients.Add(cocktailIngredient);
            return this;
        }

        public CocktailBuilder AddIngredients(List<CocktailIngredient> ingredients)
        {
            foreach (var ingredient in ingredients)
                AddIngredient(ingredient);
            return this;
        }

        public Cocktail Build()
        {
            return new Cocktail(_name, _ingredients);
        }
    }
}
