
using CocktailsApp.ReadStore.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.ReadStore.Persistence
{
    public sealed class UserSummaryConfiguration : IEntityTypeConfiguration<UserSummaryRead>
    {
        public void Configure(EntityTypeBuilder<UserSummaryRead> builder)
        {
            // PK
            builder.HasKey(u => u.UserId);

            // properties
            builder.Property(u => u.Username)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(u => u.ImageId)
                .HasMaxLength(200);

            // Indexes
            builder.HasIndex(u => u.UserId);
        }
    }
}
