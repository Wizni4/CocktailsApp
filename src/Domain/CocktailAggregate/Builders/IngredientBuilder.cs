/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.CocktailAggregate
{
    public class IngredientBuilder : IBuilder<Ingredient>
    {
        private string _name = "";
        private string _unit = "";

        public IngredientBuilder AddName(string name)
        {
            _name = name;
            return this;
        }

        public IngredientBuilder AddUnit(string unit)
        {
            _unit = unit;
            return this;
        }

        public Ingredient Build()
        {
            return new Ingredient(_name, _unit);
        }
    }
}