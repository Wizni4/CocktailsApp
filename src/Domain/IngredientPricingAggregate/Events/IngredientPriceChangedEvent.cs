/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.IngredientPricingAggregate
{
    public sealed record IngredientPriceChangedEvent(
        Guid IngredientId,
        decimal OldPrice,
        decimal NewPrice,
        Guid ActorId
    ) : DomainEvent(IngredientId, typeof(IngredientPricing), ActorId);
}
