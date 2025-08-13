/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public sealed record ClubRoleDeletedEvent(
        Guid ClubId,
        Guid RoleId,
        Guid ActorId
    ) : DomainEvent(ClubId, typeof(Club), ActorId);
}
