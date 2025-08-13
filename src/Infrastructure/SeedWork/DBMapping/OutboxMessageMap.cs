using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.SeedWork
{
    public class OutboxMessageMap : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("Outbox");

            // PK
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.OccurredOn)
                .IsRequired();
            builder.Property(x => x.AggregateType)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.AggregateId)
                .IsRequired();
            builder.Property(x => x.AggregateVersion);
            builder.Property(x => x.ActorId);

            builder.Property(x => x.Type)
                .HasMaxLength(300)
                .IsRequired();
            builder.Property(x => x.Payload)
                .IsRequired();

            builder.Property(x => x.ProcessedOn);
            builder.Property(x => x.AttemptCount)
                .HasDefaultValue(0);

            // Indexes
            // -- For stream-ordered replays
            builder.HasIndex(x => new { x.AggregateType, x.AggregateId, x.OccurredOn });

            // -- Fast scan of pending messages (SQL Server filtered index)
            builder.HasIndex(x => new { x.ProcessedOn, x.OccurredOn })
             .HasFilter("[ProcessedOn] IS NULL");
        }
    }
}
