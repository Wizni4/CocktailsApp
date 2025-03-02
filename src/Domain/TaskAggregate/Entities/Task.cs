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
using System.Text.RegularExpressions;

namespace Domain.TaskAggregate
{
    public class Task : Entity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatus Status { get; private set; }
        private List<Item> _items = new List<Item>();
        public IReadOnlyCollection<Item> Items { get { return _items.AsReadOnly(); } }
        internal Task(string title, string description)
        {
            Title = title;
            Description = description;
            Status = TaskStatus.Open;
        }

        public void ChangeItemStatus(Guid itemId, TaskStatus newStatus)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                throw new InvalidOperationException("Item not found.");
            }

            // Apply any business rules or validation here
            item.ChangeStatus(newStatus);
        }
    }
}