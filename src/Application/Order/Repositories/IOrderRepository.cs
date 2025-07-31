/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;

using DomainOrder = CocktailsApp.Domain.OrderAggregate.Order;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Order
{
    public interface IOrderRepository : IRepository<DomainOrder>
    {
    }
}
