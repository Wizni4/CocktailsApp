/*
 * Framework namespaces
 */
using System;
using System.Collections.Generic;

namespace Domain.TaskAggregate
{
    public enum TaskStatus
    {
        Open = 0,
        Pending = 0,
        InProgress = 1,
        Completed = 2,
    }
}