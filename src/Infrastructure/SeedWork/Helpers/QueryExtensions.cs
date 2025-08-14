/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.Common;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;
/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.SeedWork
{
    public static class QueryExtensions
    {
        public static IQueryable<T> IncludeMultiples<T>(this IQueryable<T> query, Func<IIncludable<T>, IIncludable> includes) where T : Entity
        {
            if (includes == null)
                return query;

            var includable = (Includable<T>)includes(new Includable<T>(query));
            return includable.Input.AsSplitQuery();
        }

        public static IQueryable<T> ApplySorts<T>(this IQueryable<T> q, IReadOnlyList<Sort<T>> sorts)
        {
            if (sorts is null || sorts.Count == 0) return q;

            IOrderedQueryable<T>? ordered = null;
            for (int i = 0; i < sorts.Count; i++)
            {
                var s = sorts[i];
                ordered = i == 0
                    ? (s.Desc ? q.OrderByDescending(s.Key) : q.OrderBy(s.Key))
                    : (s.Desc ? ordered!.ThenByDescending(s.Key) : ordered!.ThenBy(s.Key));
            }
            return ordered ?? q;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> q, int? skip, int? take)
        {
            if (skip.HasValue) q = q.Skip(skip.Value);
            if (take.HasValue) q = q.Take(take.Value);
            return q;
        }
    }
}
