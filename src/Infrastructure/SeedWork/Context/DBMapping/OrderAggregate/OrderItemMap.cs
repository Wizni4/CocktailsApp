/*
 * Domain namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.IngredientPricingAggregate;
using CocktailsApp.Domain.OrderAggregate;


/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.SeedWork
{
    /// <summary>
    /// Represents the configuration for the <see cref="OrderItem"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        /// <summary>
        /// Configures the entity of type <see cref="OrderItem"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="OrderItem"/> entity</param>
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // PK
            builder.HasKey(oi => oi.Id);

            // Properties
            builder.Property(oi => oi.Quantity).HasPrecision(18, 4);

            // FK
            builder.HasOne<Cocktail>()
                .WithMany()
                .HasForeignKey(oi => oi.CocktailId);
           
        }
    }
}
