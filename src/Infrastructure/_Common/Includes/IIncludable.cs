using CocktailsApp.Domain.Common;

using System.Linq.Expressions;

namespace CocktailsApp.Infrastructure.Common
{
    public interface IIncludable
    {
    }

    public interface IIncludable<TEntity> : IIncludable where TEntity : Entity, IAggregateRoot
    {
        IQueryable<TEntity> Input { get; set; }
        IIncludable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector);
        IIncludable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> propertySelector);
        IIncludable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IReadOnlyCollection<TProperty>>> propertySelector);
    }

    public interface IIncludable<TEntity, TProperty> : IIncludable<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IIncludable<TEntity, TOtherProperty> ThenInclude<TOtherProperty>(Expression<Func<TProperty, TOtherProperty>> propertySelector);
        IIncludable<TEntity, TOtherProperty> ThenInclude<TOtherProperty>(Expression<Func<TProperty, IEnumerable<TOtherProperty>>> propertySelector);
        IIncludable<TEntity, TOtherProperty> ThenInclude<TOtherProperty>(Expression<Func<TProperty, IReadOnlyCollection<TOtherProperty>>> propertySelector);
    }
}
