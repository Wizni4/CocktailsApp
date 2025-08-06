/*
 * Domain namespaces
 */
using CocktailsApp.Domain.IngredientAggregate;
using CocktailsApp.Domain.IngredientPricingAggregate;
using CocktailsApp.Infrastructure.SeedWork;


/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.IngredientPricingAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="IngredientPricing"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class IngredientPricingMap : EntityMap<IngredientPricing>
    {
        /// <summary>
        /// Configures the entity of type <see cref="IngredientPricing"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="IngredientPricing"/> entity</param>
        public override void Configure(EntityTypeBuilder<IngredientPricing> builder)
        {
            base.Configure(builder);

            // Propeties
            builder.Property(ip => ip.Cost)
                .HasPrecision(18, 4)
                .IsRequired();
            builder.Property(ip => ip.Price)
                .HasPrecision(18, 4)
                .IsRequired();

            // FK
            // -- Ingredient
            builder.HasOne<Ingredient>()
                .WithMany()
                .HasForeignKey(ip => ip.IngredientId)
                .IsRequired();
        }
    }
}
