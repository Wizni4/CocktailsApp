/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.SeedWork
{
    public interface IQuery
    {
    }
    public interface IQuery<TDomain> : IQuery
    {
        ISpecification<TDomain> Specification { get; }
        Func<IIncludable<TDomain>, IIncludable>? Include { get; }
    }
}
