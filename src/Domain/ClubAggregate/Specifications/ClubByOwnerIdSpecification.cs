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
    public class ClubByOwnerIdSpecification(Guid ownerId) : Specification<Club>
    {
        private readonly Guid _ownerId = ownerId;
        public override Expression<Func<Club, bool>> SpecExpression
        {
            get
            {
                return club => club.Members
                            .Where(m => m.Id == _ownerId || m.UserId == _ownerId)
                            .SelectMany(m => m.Roles)
                            .Any(r => r.IsOwnerRole);
            }
        }
    }
}
