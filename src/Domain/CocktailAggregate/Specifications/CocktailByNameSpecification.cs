/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class CocktailByNameSpecification(string name) : Specification<Cocktail>
    {
        private readonly string _name = name;
        public override Expression<Func<Cocktail, bool>> SpecExpression
        {
            get
            {
                return cocktail => cocktail.Name == _name;
            }
        }
    }
}
