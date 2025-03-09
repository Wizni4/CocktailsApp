/*
 * Framework namespaces
 */
using System.Linq.Expressions;

namespace Domain.SeedWork
{
    /// <summary>
    /// Represents the base interface for the inclusion mechanism in queries.
    /// </summary>
    public interface IIncludable
    {
    }
    /// <summary>
    /// Interface that allows for including related entities in a query for an entity of type <typeparamref name="TEntity"/>.<br/>
    /// This is used to specify properties that should be eagerly loaded during a query.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which related properties have to be included.</typeparam>
    public interface IIncludable<TEntity> : IIncludable
    {
        /// <summary>
        /// The IQueryable instance representing the entity and its related entities that can be further manipulated.
        /// </summary>
        IQueryable<TEntity> Input { get; set; }
        /// <summary>
        /// Includes a related property in the query for eager loading.
        /// </summary>
        /// <typeparam name="TProperty">The type of the related property to include.</typeparam>
        /// <param name="propertySelector">The expression to specify the related property to include.</param>
        /// <returns>A new instance of <see cref="IIncludable{TEntity, TProperty}"/> representing the included property.</returns>
        IIncludable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector);
        /// <summary>
        /// Includes a related collection property in the query for eager loading.
        /// </summary>
        /// <typeparam name="TProperty">The type of the collection property to include.</typeparam>
        /// <param name="propertySelector">The expression to specify the related collection property to include.</param>
        /// <returns>A new instance of <see cref="IIncludable{TEntity, TProperty}"/> representing the included collection property.</returns>
        IIncludable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> propertySelector);
        /// <summary>
        /// Includes a related collection property in the query for eager loading with a read-only collection type.
        /// </summary>
        /// <typeparam name="TProperty">The type of the read-only collection property to include.</typeparam>
        /// <param name="propertySelector">The expression to specify the related read-only collection property to include.</param>
        /// <returns>A new instance of <see cref="IIncludable{TEntity, TProperty}"/> representing the included read-only collection property.</returns>
        IIncludable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IReadOnlyCollection<TProperty>>> propertySelector);
    }

    /// <summary>
    /// Interface that allows for including multiple levels of depth in a query for an entity of type <typeparamref name="TEntity"/>.<br/>
    /// Allows to include additional levels of related <typeparamref name="TEntity"/>.<br/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type for which related properties are to be included.</typeparam>
    /// <typeparam name="TProperty">The type of the related property that is included.</typeparam>
    public interface IIncludable<TEntity, TProperty> : IIncludable<TEntity>
    {
        /// <summary>
        /// Includes a related property of type <typeparamref name="TOtherProperty"/> in the query at the next level of depth.
        /// </summary>
        /// <typeparam name="TOtherProperty">The type of the related property at the next level.</typeparam>
        /// <param name="propertySelector">The expression to specify the next level related property to include.</param>
        /// <returns>A new instance of <see cref="IIncludable{TEntity, TOtherProperty}"/> representing the included related property.</returns>
        IIncludable<TEntity, TOtherProperty> ThenInclude<TOtherProperty>(Expression<Func<TProperty, TOtherProperty>> propertySelector);
        /// <summary>
        /// Includes a related collection property of type <typeparamref name="TOtherProperty"/> in the query at the next level of depth.
        /// </summary>
        /// <typeparam name="TOtherProperty">The type of the related collection property at the next level.</typeparam>
        /// <param name="propertySelector">The expression to specify the next level related collection property to include.</param>
        /// <returns>A new instance of <see cref="IIncludable{TEntity, TOtherProperty}"/> representing the included related collection property.</returns>
        IIncludable<TEntity, TOtherProperty> ThenInclude<TOtherProperty>(Expression<Func<TProperty, IEnumerable<TOtherProperty>>> propertySelector);
        /// <summary>
        /// Includes a related collection property of type <typeparamref name="TOtherProperty"/> in the query at the next level of depth with a read-only collection type.
        /// </summary>
        /// <typeparam name="TOtherProperty">The type of the related read-only collection property at the next level.</typeparam>
        /// <param name="propertySelector">The expression to specify the next level related read-only collection property to include.</param>
        /// <returns>A new instance of <see cref="IIncludable{TEntity, TOtherProperty}"/> representing the included related read-only collection property.</returns>
        IIncludable<TEntity, TOtherProperty> ThenInclude<TOtherProperty>(Expression<Func<TProperty, IReadOnlyCollection<TOtherProperty>>> propertySelector);
    }
}
