
using CocktailsApp.ReadStore.Ingredients;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.ReadStore.Persistence
{
    public sealed class IngredientReadConfiguration : IEntityTypeConfiguration<IngredientRead>
    {
        public void Configure(EntityTypeBuilder<IngredientRead> builder)
        {
            // PK
            builder.HasKey(i => i.IngredientId);

            // Properties
            builder.Property(i => i.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(i => i.IngredientType)
               .HasMaxLength(200)
               .IsRequired();
            builder.Property(i => i.ImageId)
               .HasMaxLength(200);
            builder.Property(i => i.IsAlcoholic)
               .IsRequired();

            // Indexes
            builder.HasIndex(i => i.IngredientId);
        }
    }
}
