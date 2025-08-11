/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;
/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using System.Linq.Expressions;

namespace CocktailsApp.Infrastructure.SeedWork
{
    public class Includable<TEntity> : IIncludable<TEntity> where TEntity : Entity
    {
        public IQueryable<TEntity> Input { get; set; }

        public Includable(IQueryable<TEntity> queryable)
        {
            Input = queryable;
        }

        public IIncludable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            var result = Input.Include(propertySelector);
            return new Includable<TEntity, TProperty>(result);
        }

        public IIncludable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> propertySelector)
        {
            var result = Input.Include(propertySelector);
            return new Includable<TEntity, TProperty>(result);
        }

        public IIncludable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IReadOnlyCollection<TProperty>>> propertySelector)
        {
            var result = Input.Include(propertySelector);
            return new Includable<TEntity, TProperty>(result);
        }
    }

    public class Includable<TEntity, TProperty> : Includable<TEntity>, IIncludable<TEntity, TProperty> where TEntity : Entity
    {
        private readonly IIncludableQueryable<TEntity, TProperty>? _includableInput;
        private readonly IIncludableQueryable<TEntity, IEnumerable<TProperty>>? _includableListInput;

        public Includable(IIncludableQueryable<TEntity, TProperty> queryable) : base(queryable)
        {
            _includableInput = queryable;
        }

        public Includable(IIncludableQueryable<TEntity, IEnumerable<TProperty>> queryable) : base(queryable)
        {
            _includableListInput = queryable;
        }

        public IIncludable<TEntity, TOtherProperty> ThenInclude<TOtherProperty>(Expression<Func<TProperty, TOtherProperty>> propertySelector)
        {
            IIncludableQueryable<TEntity, TOtherProperty> result = null!;

            if (_includableInput != null)
                result = _includableInput.ThenInclude(propertySelector);
            else
                result = _includableListInput!.ThenInclude(propertySelector);

            return new Includable<TEntity, TOtherProperty>(result);
        }

        public IIncludable<TEntity, TOtherProperty> ThenInclude<TOtherProperty>(Expression<Func<TProperty, IEnumerable<TOtherProperty>>> propertySelector)
        {
            IIncludableQueryable<TEntity, IEnumerable<TOtherProperty>> result = null!;

            if (_includableInput != null)
                result = _includableInput.ThenInclude(propertySelector);
            else
                result = _includableListInput!.ThenInclude(propertySelector);

            return new Includable<TEntity, TOtherProperty>(result);
        }

        public IIncludable<TEntity, TOtherProperty> ThenInclude<TOtherProperty>(Expression<Func<TProperty, IReadOnlyCollection<TOtherProperty>>> propertySelector)
        {
            IIncludableQueryable<TEntity, IReadOnlyCollection<TOtherProperty>> result = null!;

            if (_includableInput != null)
                result = _includableInput.ThenInclude(propertySelector);
            else
                result = _includableListInput!.ThenInclude(propertySelector);

            return new Includable<TEntity, TOtherProperty>(result);
        }
    }
}
