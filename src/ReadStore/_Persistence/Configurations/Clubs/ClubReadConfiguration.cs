

using CocktailsApp.ReadStore.Clubs;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.ReadStore.Persistence
{
    public sealed class ClubReadConfiguration : IEntityTypeConfiguration<ClubRead>
    {
        public void Configure(EntityTypeBuilder<ClubRead> builder)
        {
            // PK
            builder.HasKey(c => c.ClubId);

            // Property
            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(c => c.Description)
                .HasMaxLength(2000);
            builder.Property(c => c.Visibility)
                .IsRequired();
            builder.Property(c => c.Street)
                .HasMaxLength(200);
            builder.Property(c => c.StreetNumber)
                .HasMaxLength(200);
            builder.Property(c => c.City)
                .HasMaxLength(200);
            builder.Property(c => c.PostalCode)
                .HasMaxLength(200);
            builder.Property(c => c.State)
                .HasMaxLength(200);
            builder.Property(c => c.Country)
                .HasMaxLength(200);
            builder.Property(c => c.ImageId)
                .HasMaxLength(200);

            // Indexes
            builder.HasIndex(c => c.ClubId);
        }
    }
}
