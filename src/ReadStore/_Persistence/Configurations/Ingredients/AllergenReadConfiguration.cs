


using CocktailsApp.ReadStore.Ingredients;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailsApp.ReadStore.Persistence
{
    public sealed class AllergenReadConfiguration : IEntityTypeConfiguration<AllergenRead>
    {
        public void Configure(EntityTypeBuilder<AllergenRead> builder)
        {
            // PK
            builder.HasKey("IngredientId", "Name");

            // Properties
            builder.Property(i => i.Name)
                .HasMaxLength(200)
                .IsRequired();

            // FK
            builder.HasOne<IngredientRead>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(i => i.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(i => i.IngredientId);
        }
    }
}
