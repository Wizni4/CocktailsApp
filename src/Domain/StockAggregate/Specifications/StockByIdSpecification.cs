/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.StockAggregate
{
    public class StockByIdSpecification(Guid id) : ByIdSpecification<Stock>(id)
    {
    }
}
