/*
 * Domain namespaces
 */
using CocktailsApp.Domain.IngredientPricingAggregate;

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
    public class IngredientPricingMap : IEntityTypeConfiguration<IngredientPricing>
    {
        /// <summary>
        /// Configures the entity of type <see cref="IngredientPricing"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="IngredientPricing"/> entity</param>
        public void Configure(EntityTypeBuilder<IngredientPricing> builder)
        {
            // PK
            builder.HasKey(ip => ip.Id);

            // Propeties
            builder.Property(ip => ip.Cost).HasPrecision(18, 4);
            builder.Property(ip => ip.Price).HasPrecision(18, 4);
            builder.Property(cc => cc.CreationDate);
            builder.Property(cc => cc.UpdateDate);

            // Value object
            builder.ComplexProperty(ip => ip.Ingredient, a =>
            {
                a.IsRequired();

                a.Property(i => i.Name).IsRequired();
            });
        }
    }
}
