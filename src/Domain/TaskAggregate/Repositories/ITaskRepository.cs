/*
 * Domain namespaces
 */
using Domain.SeedWork;
using System;
/*
* Framework namespaces
*/

namespace Domain.TaskAggregate
{
    public interface ITaskRepository : IRepository<Task>
    {
    }
}
