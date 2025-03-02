/*
 * Domain namespaces
 */
using Domain.SeedWork;
/*
* Framework namespaces
*/
using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain.TaskAggregate
{
    public class Item : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatus Status { get; private set; }
        internal Item(string title, string description)
        {
            Title = title;
            Description = description;
            Status = TaskStatus.Open;
        }

        internal void ChangeStatus(TaskStatus newStatus)
        {
            // Business logic to validate status transition
            if (newStatus == TaskStatus.Completed && Status != TaskStatus.InProgress)
            {
                throw new InvalidOperationException("Item can only be marked as Completed if it is InProgress.");
            }

            Status = newStatus;
        }
    }
}