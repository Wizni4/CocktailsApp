/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;


namespace CocktailsApp.Application.Club
{
    public class RolePermissionsUpdateModel
    {
        public required Guid Id { get; set; }
        public required IEnumerable<ClubPermissionType> Permissions { get; set; }
    }
}
