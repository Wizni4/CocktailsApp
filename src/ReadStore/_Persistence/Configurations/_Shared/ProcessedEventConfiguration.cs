
using CocktailsApp.ReadStore.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.ReadStore.Persistence
{
    public sealed class ProcessedEventConfiguration : IEntityTypeConfiguration<ProcessedEvent>
    {
        public void Configure(EntityTypeBuilder<ProcessedEvent> builder)
        {
            // PK
            builder.HasKey(x => x.EventId);

            // Properties
            builder.Property(x => x.Topic);
            builder.Property(x => x.Partition);
            builder.Property(x => x.Offset);
            builder.Property(x => x.ProcessedOnUtc);
        }
    }
}
