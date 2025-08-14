

using CocktailsApp.Application.Common;
using CocktailsApp.Domain.OrderAggregate;

namespace CocktailsApp.Application.Orders
{
    public interface IOrderRepository : IRepository<Order>;
}
