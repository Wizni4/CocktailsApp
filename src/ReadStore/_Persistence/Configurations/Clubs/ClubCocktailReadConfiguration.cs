

using CocktailsApp.ReadStore.Clubs;
using CocktailsApp.ReadStore.Cocktails;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.ReadStore.Persistence
{
    public sealed class ClubCocktailReadConfiguration : IEntityTypeConfiguration<ClubCocktailRead>
    {
        public void Configure(EntityTypeBuilder<ClubCocktailRead> builder)
        {
            // PK
            builder.HasKey(c => c.ClubCocktailId);

            // FK
            // -- Club
            builder.HasOne<ClubRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            // -- Cocktail
            builder.HasOne<CocktailRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.CocktailId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(c => c.ClubId);
            builder.HasIndex(c => c.ClubCocktailId);
            builder.HasIndex(c => c.CocktailId);
        }
    }
}
