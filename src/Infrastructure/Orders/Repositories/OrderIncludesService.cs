
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Orders;
using CocktailsApp.Infrastructure.Common;

namespace CocktailsApp.Infrastructure.Orders
{
    public class OrderIncludesService : IIncludesService<Order>
    {
        public Func<IIncludable<Order>, IIncludable> GetIncludes(IEnumerable<ILoad<Order>> graph)
        {
            throw new NotImplementedException();
        }
    }
}
