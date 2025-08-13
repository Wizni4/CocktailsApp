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
    public class ClubRoleByIdSpecification(Guid roleId) : ByIdSpecification<ClubRole>(roleId)
    {
    }
}
