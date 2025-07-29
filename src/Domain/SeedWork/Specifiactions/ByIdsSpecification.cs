/*
 * Framework namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;

using System.Linq.Expressions;

namespace CocktailsApp.Domain.SeedWork
{
    public class ByIdsSpecification<T>(IEnumerable<Guid> ids) : Specification<T> where T : Entity
    {
        private readonly IEnumerable<Guid> _ids = ids;

        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                return entity => _ids.Contains(entity.Id);
            }
        }
    }
}
