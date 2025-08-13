/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.StockAggregate
{
    public sealed record IngredientOutOfStock(
        Guid StockId,
        Guid IngredientId,
        Guid ActorId
    ) : DomainEvent(StockId, typeof(Stock), ActorId);
}
