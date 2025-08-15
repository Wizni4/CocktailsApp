

using CocktailsApp.ReadStore.Clubs;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.ReadStore.Persistence
{
    public sealed class ClubRoleReadConfiguration : IEntityTypeConfiguration<ClubRoleRead>
    {
        public void Configure(EntityTypeBuilder<ClubRoleRead> builder)
        {
            // PK
            builder.HasKey(c => c.RoleId);

            // properties
            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(c => c.IsOwnerRole)
                .IsRequired();

            // FK
            // -- Club
            builder.HasOne<ClubRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(c => c.ClubId);
            builder.HasIndex(c => c.RoleId);
        }
    }

    public sealed class ClubRolePermissionReadConfiguration : IEntityTypeConfiguration<ClubRolePermissionRead>
    {
        public void Configure(EntityTypeBuilder<ClubRolePermissionRead> builder)
        {
            // PK
            builder.HasKey("RoleId", "Permission");

            // Property
            builder.Property(c => c.Permission)
                .HasMaxLength(200)
                .IsRequired();

            // FK
            // -- Club
            builder.HasOne<ClubRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            // -- Role
            builder.HasOne<ClubRoleRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(c => c.ClubId);
            builder.HasIndex(c => c.RoleId);
        }
    }
}
