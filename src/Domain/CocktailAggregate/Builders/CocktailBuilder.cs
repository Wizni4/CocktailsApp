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
        private Cocktail? _cocktail = null;
        private string _name = "";
        private string _description = "";

        public CocktailBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }

        public CocktailBuilder WithDescription(string description)
        {
            this._description = description;
            return this;
        }

        public CocktailBuilder AddIngredient(Ingredient ingredient, decimal quantity)
        {
            TryCreateCocktail();

            if (_cocktail == null)
                throw new InvalidOperationException("Cocktail must have a name and a description before adding ingrdients");

            _cocktail.AddIngredient(ingredient, quantity);
            return this;
        }

        public Cocktail Build()
        {
            if (_cocktail == null)
                throw new InvalidOperationException("Cocktail must have a name and description.");

            return _cocktail;
        }

        private void TryCreateCocktail()
        {
            // Create Cocktail instance as soon as we have both required fields
            if (!string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_description) && _cocktail == null)
                _cocktail = new Cocktail(_name, _description);
        }

    }
}
