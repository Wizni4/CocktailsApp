/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    public sealed record ClubRoleDeletedEvent(
        Guid ClubId,
        Guid RoleId,
        Guid ActorId
    ) : DomainEvent(ClubId, typeof(Club), ActorId);
}
