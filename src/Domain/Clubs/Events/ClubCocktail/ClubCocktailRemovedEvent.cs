/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    public sealed record ClubCocktailRemovedEvent(
        Guid ClubId,
        Guid ClubCocktailId,
        Guid ActorId
    ) : DomainEvent(ClubId, typeof(Club), ActorId);
}
