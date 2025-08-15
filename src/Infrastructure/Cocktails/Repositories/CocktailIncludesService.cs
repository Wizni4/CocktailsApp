
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Infrastructure.Common;

namespace CocktailsApp.Infrastructure.Cocktails
{
    public sealed class CocktailIncludesService : IIncludesService<Cocktail>
    {
        public Func<IIncludable<Cocktail>, IIncludable> GetIncludes(IEnumerable<ILoad<Cocktail>> graph)
        {
            throw new NotImplementedException();
        }
    }
}
