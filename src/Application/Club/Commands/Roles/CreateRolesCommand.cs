/*
 * Framework namespaces
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
    public record CreateRolesCommand(
        Guid ClubId,
        IEnumerable<CreateRoleModel> NewRoles,
        Guid ActorId
    ) : ClubCommand<IEnumerable<Guid>>(ClubId, ActorId);

    public class CreateRoleModel
    {
        public required string Name { get; set; }
        public IEnumerable<ClubPermissionType>? Permissions { get; set; }
    }
}
