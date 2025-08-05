/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class ClubByUserIdSpecification(Guid userId) : Specification<DomainClub>
    {
        private readonly Guid _userId = userId;

        public override Expression<Func<DomainClub, bool>> SpecExpression => c => c.Members.Any(m => m.UserId == _userId);

    }
}
