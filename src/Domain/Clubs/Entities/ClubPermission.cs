/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    /// <summary>
    /// Enumerates the permissions that can be assigned to roles or members within a club.
    /// </summary>
    public sealed class ClubPermission : ValueObject
    {
        public PermissionType Permission { get; }

        internal ClubPermission(PermissionType permission)
        {
            Permission = permission;
        }
    }
}
