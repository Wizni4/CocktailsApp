/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;

using Microsoft.EntityFrameworkCore;
/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.SeedWork
{
    public static class QueryExtensions
    {
        public static IQueryable<T> IncludeMultiples<T>(this IQueryable<T> query, Func<IIncludable<T>, IIncludable> includes) where T : Entity, IAggregateRoot
        {
            if (includes == null)
                return query;

            var includable = (Includable<T>)includes(new Includable<T>(query));
            return includable.Input.AsSplitQuery();
        }
    }
}
