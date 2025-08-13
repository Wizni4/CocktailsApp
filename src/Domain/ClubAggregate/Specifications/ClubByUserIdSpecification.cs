/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubByUserIdSpecification(Guid userId) : Specification<Club>
    {
        private readonly Guid _userId = userId;

        public override Expression<Func<Club, bool>> SpecExpression => c => c.Members.Any(m => m.UserId == _userId);

    }
}
