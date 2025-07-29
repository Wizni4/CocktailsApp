/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    /// <summary>
    /// Enumerates the permissions that can be assigned to roles or members within a club.
    /// </summary>
    public class ClubPermission : ValueObject
    {
        public ClubPermissionType Permission { get; }

        internal ClubPermission(ClubPermissionType permission)
        {
            Permission = permission;
        }
    }
}
