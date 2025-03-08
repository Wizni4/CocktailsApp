/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

namespace Application.SeedWork
{
    public interface IQueryHandler<TQuery, TDTO> where TQuery : IQuery
    {
        Task<TDTO> Handle(TQuery query);
    }
}
