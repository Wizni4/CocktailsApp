/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Stocks
{
    public sealed record IngredientOutOfStock(
        Guid StockId,
        Guid IngredientId,
        Guid ActorId
    ) : DomainEvent(StockId, typeof(Stock), ActorId);
}
