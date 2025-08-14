/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Cocktails
{
    public class CocktailIngredientFactory : IFactory<CocktailIngredient>
    {
        private Guid _ingredientId;
        private Guid _creatorId;
        private decimal _quantity = 0;
        private UnitOfMeasure _unit = (UnitOfMeasure)9999;

        public CocktailIngredientFactory WithCreatorId(Guid creatorId)
        {
            _creatorId = creatorId;
            return this;
        }

        public CocktailIngredientFactory WithIngredient(Guid ingredientId)
        {
            _ingredientId = ingredientId;
            return this;
        }

        public CocktailIngredientFactory WithQuantity(decimal quantity)
        {
            _quantity = quantity;
            return this;
        }

        public CocktailIngredientFactory WithUnit(UnitOfMeasure unit)
        {
            _unit = unit;
            return this;
        }

        public CocktailIngredient Build()
        {
            return new CocktailIngredient(_ingredientId, _quantity, _unit, _creatorId);
        }
    }
}
