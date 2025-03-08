/*
 * Domain namespaces
 */
using System.Linq.Expressions;

using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.CocktailAggregate
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
