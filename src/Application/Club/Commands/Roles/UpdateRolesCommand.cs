/*
*Framework namespaces
*/
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;

namespace CocktailsApp.Application.Club
{
    public record UpdateRolesCommand(
        Guid ClubId,
        IEnumerable<UpdateRoleModel> Roles,
        Guid ActorId
    ) : ICommand<IEnumerable<ClubRoleDTO>>;

    public class UpdateRoleModel
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<ClubPermissionType>? Permissions { get; set; }
    }
}
