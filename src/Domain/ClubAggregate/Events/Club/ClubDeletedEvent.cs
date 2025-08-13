/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public sealed record ClubDeletedEvent(
        Guid ClubId,
        Guid ActorId
    ) : DomainEvent(ClubId, typeof(Club), ActorId);
}
