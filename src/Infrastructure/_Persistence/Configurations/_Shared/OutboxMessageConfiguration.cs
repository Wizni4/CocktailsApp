

using CocktailsApp.Infrastructure.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.Infrastructure.Persistence
{
    public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            // PK
            builder.HasKey(x => x.Id);

            // properties
            builder.Property(x => x.OccurredOn);
            builder.Property(x => x.AggregateType);
            builder.Property(x => x.AggregateId);
            builder.Property(x => x.AggregateVersion);
            builder.Property(x => x.ActorId);
            builder.Property(x => x.Type);
            builder.Property(x => x.Payload);
            builder.Property(x => x.ProcessedOn);
            builder.Property(x => x.AttemptCount);
        }
    }
}
