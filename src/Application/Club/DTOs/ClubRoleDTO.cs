/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;
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
    public sealed class ClubRoleDTO : EntityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> Permissions { get; set; } = default!;
    }
}
