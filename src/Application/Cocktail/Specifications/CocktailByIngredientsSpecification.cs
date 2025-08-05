/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;
using System.Linq.Expressions;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public class CocktailByIngredientsSpecification(List<Ingredient> ingredients) : Specification<DomainCocktail>
    {
        private readonly IEnumerable<Ingredient> _ingredients = ingredients;

        public override Expression<Func<DomainCocktail, bool>> SpecExpression
        {
            get
            {
                return cocktail => _ingredients.All(ingredient => cocktail.Ingredients.Any(ci => ci.Ingredient.Name == ingredient.Name));
            }
        }
    }
}
