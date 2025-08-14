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
    public class ClubRoleByIdSpecification(Guid roleId) : ByIdSpecification<ClubRole>(roleId)
    {
    }
}
