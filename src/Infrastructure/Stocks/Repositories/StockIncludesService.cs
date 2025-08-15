

using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Stocks;
using CocktailsApp.Infrastructure.Common;

namespace CocktailsApp.Infrastructure.Stocks
{
    public sealed class StockIncludesService : IIncludesService<Stock>
    {
        public Func<IIncludable<Stock>, IIncludable> GetIncludes(IEnumerable<ILoad<Stock>> graph)
        {
            throw new NotImplementedException();
        }
    }
}
