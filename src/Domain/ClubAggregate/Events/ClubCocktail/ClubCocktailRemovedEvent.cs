/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public sealed record ClubCocktailRemovedEvent(
        Guid ClubId,
        Guid ClubCocktailId,
        Guid ActorId
    ) : DomainEvent(ClubId, typeof(Club), ActorId);
}
