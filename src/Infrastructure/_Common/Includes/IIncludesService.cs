

using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Common;

namespace CocktailsApp.Infrastructure.Common
{
    public interface IIncludesService<T> where T : Entity, IAggregateRoot
    {
        Func<IIncludable<T>, IIncludable> GetIncludes(IEnumerable<ILoad<T>> graph);
    }
}
