/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a role within a club.
    /// A role defines a set of permissions and a unique name.
    /// Inherits from <see cref="EntityDTO"/> to include common entity identification.
    /// </summary>
    public class ClubRoleDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name of the role (e.g., "Administrator", "Moderator").
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the list of permissions associated with this role.
        /// </summary>
        public List<ClubPermissionType> Permissions { get; set; } = [];
    }
}
