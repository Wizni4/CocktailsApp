/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;


/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace CocktailsApp.Domain.IngredientPricingAggregate
{
    public class IngredientPricingSpecification(List<Ingredient> ingredients) : Specification<IngredientPricing>
    {
        private readonly List<Ingredient> _ingredients = ingredients;

        public override Expression<Func<IngredientPricing, bool>> SpecExpression
        {
            get
            {
                return ingredientPricing => _ingredients.Any(ingredient => ingredientPricing.Ingredient == ingredient);
            }
        }
    }
}
