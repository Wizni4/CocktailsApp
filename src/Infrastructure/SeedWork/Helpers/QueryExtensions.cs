/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;

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

        public static IQueryable<TFather> Select<TFather>(this IQueryable<TFather> query, Func<ISelector<TFather>, ISelector> selector)
            where TFather : Entity
        {
            var selected = (Selector<TFather>)selector(new Selector<TFather>(query));
            return selected.Input.AsSplitQuery();
        }

        public static IQueryable<TChild> SelectChild<TFather, TChild>(this IQueryable<TFather> query, Func<ISelector<TFather>, ISelector<TChild>> selector) 
            where TFather : Entity
            where TChild : Entity
        {
            var selected = (Selector<TChild>)selector(new Selector<TFather>(query));
            return selected.Input.AsSplitQuery();
        }

        public static IQueryable<Pair<TLeft, TRight?>> LeftJoinPair<TLeft, TRight, TKey>(
            this IQueryable<TLeft> left,
            IQueryable<TRight> right,
            Expression<Func<TLeft, TKey>> leftKey,
            Expression<Func<TRight, TKey>> rightKey)
            where TLeft : class
            where TRight : class
        {
            return left
                .GroupJoin(right, leftKey, rightKey, (l, r) => new { l, r })
                .SelectMany(x => x.r.DefaultIfEmpty(), (x, r) =>
                    new Pair<TLeft, TRight?> { Left = x.l, Right = r });
        }
    }
}
