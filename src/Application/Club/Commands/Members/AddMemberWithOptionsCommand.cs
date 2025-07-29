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
    public record AddMemberWithOptionsCommand(
        Guid ClubId,
        Guid NewMemberUserId,
        Guid ActorId,
        IEnumerable<Guid>? RoleIds,
        IEnumerable<ClubPermissionType>? Permissions
    ) : ICommand<ClubDTO>;
}
