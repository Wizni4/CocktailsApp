/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Cocktails
{
    public sealed record CocktailDeletedEvent(
        Guid CocktailId,
        Guid ActorId
    ) : DomainEvent(CocktailId, typeof(Cocktail), ActorId);
}
