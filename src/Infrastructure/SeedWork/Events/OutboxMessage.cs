// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsApp.Infrastructure.SeedWork
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Ordering & audit
        public DateTimeOffset OccurredOn { get; set; }
        public string AggregateType { get; set; } = default!;
        public Guid AggregateId { get; set; }
        public long? AggregateVersion { get; set; }
        public Guid? ActorId { get; set; }

        // Payload
        public string Type { get; set; } = default!;
        public string Payload { get; set; } = default!;

        // Delivery state
        public DateTimeOffset? ProcessedOn { get; set; }
        public int AttemptCount { get; set; }
    }
}
