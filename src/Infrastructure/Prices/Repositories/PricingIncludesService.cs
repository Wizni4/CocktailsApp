using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Prices;
using CocktailsApp.Infrastructure.Common;

namespace CocktailsApp.Infrastructure.Prices
{
    public sealed class PricingIncludesService : IIncludesService<Pricing>
    {
        public Func<IIncludable<Pricing>, IIncludable> GetIncludes(IEnumerable<ILoad<Pricing>> graph)
        {
            throw new NotImplementedException();
        }
    }
}
