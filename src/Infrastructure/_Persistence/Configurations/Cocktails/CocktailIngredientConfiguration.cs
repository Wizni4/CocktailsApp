
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Domain.Ingredients;
using CocktailsApp.Infrastructure.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.Persistence
{
    /// <summary>
    /// Represents the configuration for the <see cref="CocktailIngredient"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class CocktailIngredientConfiguration : EntityConfiguration<CocktailIngredient>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Cocktail"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Cocktail"/> entity</param>
        public override void Configure(EntityTypeBuilder<CocktailIngredient> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(ci => ci.Quantity)
                .HasPrecision(18, 4)
                .IsRequired();
            builder.Property(ci => ci.Unit)
                .HasConversion<string>()
                .IsRequired();

            // FK
            // -- Ingredient
            builder.HasOne<Ingredient>()
                .WithMany()
                .HasForeignKey(ci => ci.IngredientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
