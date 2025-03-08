/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

using Domain.SeedWork;

namespace Application.SeedWork
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
