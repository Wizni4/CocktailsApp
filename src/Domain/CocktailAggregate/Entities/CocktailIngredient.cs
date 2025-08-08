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
    public sealed class CocktailIngredient : Entity
    {
        public Guid IngredientId { get; }
        public decimal Quantity { get => _quantity; }
        private decimal _quantity;
        public UnitOfMeasure Unit { get => _unit; }
        private UnitOfMeasure _unit;
        private CocktailIngredient() { }
        internal CocktailIngredient(
            Guid ingredientId,
            decimal quantity,
            UnitOfMeasure unit,
            Guid createdBy
        ) : base(createdBy)
        {
            IngredientId = ingredientId;
            UpdateQuantity(quantity);
            UpdateUnit(unit);
        }

        internal void UpdateQuantity(decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be strictly positive.");

            _quantity = quantity;
            Touch();
        }

        internal void UpdateUnit(UnitOfMeasure unit)
        {
            if (!Enum.IsDefined(unit))
                throw new ArgumentException("Unit is invalid.");

            _unit = unit;
            Touch();
        }
    }
}
