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
    public record CreateRoleWithOptionsCommand(
        Guid ClubId,
        string RoleName,
        IEnumerable<ClubPermissionType>? Permissions,
        Guid ActorId
    ) : ICommand<ClubDTO>;
}
