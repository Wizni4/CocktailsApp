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
    public class ClubPermissionByTypeSpecification(ClubPermissionType permission) : Specification<ClubPermission>
    {
        private readonly ClubPermissionType _permission = permission;
        public override Expression<Func<ClubPermission, bool>> SpecExpression => p => p.Permission == _permission;
    }
}
