/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace Domain.CocktailAggregate
{
    public class CocktailByIdsSpecification(List<Guid> ids) : Specification<Cocktail>
    {
        private readonly List<Guid> _ids = ids;

        public override Expression<Func<Cocktail, bool>> SpecExpression
        {
            get
            {
                return cocktail => _ids.Any(id => cocktail.Id == id);
            }
        }
    }
}
