/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

using System.Linq.Expressions;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    public class ClubMemberByIdSpecification(Guid userId) : Specification<ClubMember>
    {
        private readonly Guid _userId = userId;
        public override Expression<Func<ClubMember, bool>> SpecExpression
        {
            get
            {
                return member => member.Id == _userId || member.UserId == _userId;
            }
        }
    }
}
