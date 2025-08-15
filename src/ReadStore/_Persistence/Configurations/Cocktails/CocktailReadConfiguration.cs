
using CocktailsApp.ReadStore.Cocktails;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.ReadStore.Persistence
{
    public sealed class CocktailReadConfiguration : IEntityTypeConfiguration<CocktailRead>
    {
        public void Configure(EntityTypeBuilder<CocktailRead> builder)
        {
            // PK
            builder.HasKey(c => c.CocktailId);

            // Property
            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(c => c.Description)
                .HasMaxLength(2000);
            builder.Property(c => c.ImageId)
               .HasMaxLength(200);

            // Indexes
            builder.HasIndex(c => c.CocktailId);
        }
    }

    public sealed class CocktailIngredientReadConfiguration : IEntityTypeConfiguration<CocktailIngredientRead>
    {
        public void Configure(EntityTypeBuilder<CocktailIngredientRead> builder)
        {
            // PK
            builder.HasKey(c => c.CocktailIngredientId);

            // Property
            builder.Property(c => c.IngredientName)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(c => c.IngredientType)
                .HasMaxLength(200);
            builder.Property(c => c.Quantity)
                .HasPrecision(18, 4)
                .IsRequired();
            builder.Property(c => c.Unit)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(c => c.IsAlcoholic)
                .IsRequired();

            // Indexes
            builder.HasIndex(c => c.CocktailId);
            builder.HasIndex(c => c.IngredientId);
            builder.HasIndex(c => c.CocktailIngredientId);
        }
    }
}
