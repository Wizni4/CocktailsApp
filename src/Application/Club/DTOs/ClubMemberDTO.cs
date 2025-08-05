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
    /// Data Transfer Object (DTO) representing a member of a club
    /// Inherits from <see cref="EntityDTO"/> to include common entity identification.
    /// </summary>
    public class ClubMemberDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the list of roles assigned to the club member.
        /// </summary>
        public List<ClubRoleDTO> Roles { get; set; } = [];

        /// <summary>
        /// Gets or sets the identifier of the user associated with this club member.
        /// </summary>
        public required Guid UserId { get; set; }
    }
}
