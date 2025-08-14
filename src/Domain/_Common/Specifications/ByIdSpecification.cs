/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace CocktailsApp.Domain.Common
{
    public class ByIdSpecification<T>(Guid id) : Specification<T> where T : Entity
    {
        private readonly Guid _id = id;

        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                return entity => entity.Id == _id;
            }
        }
    }
}
