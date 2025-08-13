/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

using System.Collections.ObjectModel;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a club{ get; set; } including its metadata, address, members, roles, cocktails, and visibility.
    /// Inherits from <see cref="EntityDTO"/> to include common entity identification.
    /// </summary>
    public sealed class ClubDTO: EntityDTO, IImageDTO
    {
        public Guid Id { get; set; } = default;
        public AddressDTO? Address { get; set; } = default;
        public List<ClubCocktailDTO> Cocktails { get; set; } = default!;
        public List<ClubMemberDTO> Members { get; set; } = default!;
        public List<ClubRoleDTO> Roles { get; set; } = default!;
        public string Description{ get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Visibility { get; set; } = default!;
        public string? ImageId { get; set; } = null;
    }
}
