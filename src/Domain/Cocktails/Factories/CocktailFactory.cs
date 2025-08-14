/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Cocktails
{
    public class CocktailFactory : IFactory<Cocktail>
    {
        private Cocktail? _cocktail = null;
        private Guid _creatorId;
        private string _name = "";
        private string? _description = null;

        public CocktailFactory WithName(string name)
        {
            this._name = name;
            return this;
        }

        public CocktailFactory WithCreatorId(Guid creatorId)
        {
            this._creatorId = creatorId;
            return this;
        }

        public CocktailFactory WithDescription(string? description)
        {
            this._description = description;
            return this;
        }

        public CocktailFactory AddIngredient(Guid ingredientId, decimal quantity, UnitOfMeasure unit)
        {
            TryCreateCocktail();

            if (_cocktail == null)
                throw new InvalidOperationException("Cocktail must have a name before adding ingrdients");

            _cocktail.AddIngredient(ingredientId, quantity, unit, _creatorId);
            return this;
        }

        public Cocktail Build()
        {
            if (_cocktail == null)
                throw new InvalidOperationException("Cocktail must have a name.");

            return _cocktail;
        }

        private void TryCreateCocktail()
        {
            // Create Cocktail instance as soon as we have both required fields
            if (!string.IsNullOrWhiteSpace(_name) && _cocktail == null)
                _cocktail = new Cocktail(_name, _description, _creatorId);
        }

    }
}
