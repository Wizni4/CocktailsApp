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
    public class ClubPermissionByTypeSpecification(PermissionType permission) : Specification<ClubPermission>
    {
        private readonly PermissionType _permission = permission;
        public override Expression<Func<ClubPermission, bool>> SpecExpression => p => p.Permission == _permission;
    }
}
