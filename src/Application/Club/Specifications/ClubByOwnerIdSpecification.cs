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
    public class ClubByOwnerIdSpecification(Guid ownerId) : Specification<DomainClub>
    {
        private readonly Guid _ownerId = ownerId;
        public override Expression<Func<DomainClub, bool>> SpecExpression
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
