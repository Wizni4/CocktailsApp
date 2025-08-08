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
    public class CocktailIngredientBuilder : IBuilder<CocktailIngredient>
    {
        private Guid _ingredientId;
        private Guid _creatorId;
        private decimal _quantity = 0;
        private UnitOfMeasure _unit = (UnitOfMeasure)9999;

        public CocktailIngredientBuilder WithCreatorId(Guid creatorId)
        {
            _creatorId = creatorId;
            return this;
        }

        public CocktailIngredientBuilder WithIngredient(Guid ingredientId)
        {
            _ingredientId = ingredientId;
            return this;
        }

        public CocktailIngredientBuilder WithQuantity(decimal quantity)
        {
            _quantity = quantity;
            return this;
        }

        public CocktailIngredientBuilder WithUnit(UnitOfMeasure unit)
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
