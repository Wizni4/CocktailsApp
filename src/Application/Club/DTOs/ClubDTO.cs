/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
using CocktailsApp.Domain.ClubAggregate;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a club, including its metadata, address, members, roles, cocktails, and visibility.
    /// Inherits from <see cref="EntityDTO"/> to include common entity identification.
    /// </summary>
    public class ClubDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the physical address of the club.
        /// </summary>
        public AddressDTO? Address { get; set; }

        /// <summary>
        /// Gets or sets the list of cocktails associated with the club.
        /// </summary>
        public List<CocktailDTO> Cocktails { get; set; } = [];

        /// <summary>
        /// Gets or sets the textual description of the club.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the club.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of members belonging to the club.
        /// </summary>
        public List<ClubMemberDTO> Members { get; set; } = [];

        /// <summary>
        /// Gets or sets the owner of the club.
        /// </summary>
        public required ClubMemberDTO Owner { get; set; }

        /// <summary>
        /// Gets or sets the list of roles available within the club.
        /// </summary>
        public List<ClubRoleDTO> Roles { get; set; } = [];

        /// <summary>
        /// Gets or sets the visibility level of the club (e.g., Public, Private).
        /// </summary>
        public ClubVisibility Visibility { get; set; }
    }
}
