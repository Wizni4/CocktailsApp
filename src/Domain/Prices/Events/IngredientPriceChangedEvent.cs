/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Prices
{
    public sealed record IngredientPriceChangedEvent(
        Guid IngredientId,
        decimal OldPrice,
        decimal NewPrice,
        Guid ActorId
    ) : DomainEvent(IngredientId, typeof(Pricing), ActorId);
}
