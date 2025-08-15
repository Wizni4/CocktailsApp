

using CocktailsApp.ReadStore.Clubs;
using CocktailsApp.ReadStore.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.ReadStore.Persistence
{
    public sealed class ClubMemberReadConfiguration : IEntityTypeConfiguration<ClubMemberRead>
    {
        public void Configure(EntityTypeBuilder<ClubMemberRead> builder)
        {
            // PK
            builder.HasKey(c => c.ClubMemberId);

            // FK
            // -- Club
            builder.HasOne<ClubRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            // -- User
            builder.HasOne<UserSummaryRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(c => c.ClubMemberId);
            builder.HasIndex(c => c.ClubId);
            builder.HasIndex(c => c.UserId);
        }
    }

    public sealed class ClubMemberRoleReadConfiguration : IEntityTypeConfiguration<ClubMemberRoleRead>
    {
        public void Configure(EntityTypeBuilder<ClubMemberRoleRead> builder)
        {
            // PK
            builder.HasKey("ClubId", "ClubMemberId", "RoleId");

            // Properties
            builder.Property(c => c.RoleName)
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
                .OnDelete(DeleteBehavior.Restrict);

            // -- Member
            builder.HasOne<ClubMemberRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.ClubMemberId)
                .OnDelete(DeleteBehavior.Cascade);

            // -- Role
            builder.HasOne<ClubRoleRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(c => c.ClubId);
            builder.HasIndex(c => c.ClubMemberId);
            builder.HasIndex(c => c.RoleId);
        }
    }
}
