

namespace CocktailsApp.Infrastructure.Common
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
