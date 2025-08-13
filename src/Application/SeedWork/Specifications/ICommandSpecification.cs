using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Application.SeedWork
{
    public interface ICommandSpecification<TEntity> where TEntity : Entity, IAggregateRoot
    {
        ISpecification<TEntity>? Specification { get; }
        Func<IIncludable<TEntity>, IIncludable>? Includes { get; }
    }
}
