using CocktailsApp.Domain.Common;


namespace CocktailsApp.Application.Common
{
    public interface ICommandSpecification<TEntity> where TEntity : Domain.Common.Entity, IAggregateRoot
    {
        ISpecification<TEntity>? Specification { get; }
        IEnumerable<ILoad<TEntity>> Graph { get; }
    }
}
