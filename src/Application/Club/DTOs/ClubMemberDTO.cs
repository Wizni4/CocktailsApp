/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.User;

using System.Collections.ObjectModel;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a member of a club
    /// Inherits from <see cref="EntityDTO"/> to include common entity identification.
    /// </summary>
    public sealed class ClubMemberDTO : EntityDTO
    {
        public Guid Id { get; set; }
        public List<ClubRoleDTO> Roles { get; set; } = default!;
        public UserDTO User { get; set; } = default!;
    }
}
