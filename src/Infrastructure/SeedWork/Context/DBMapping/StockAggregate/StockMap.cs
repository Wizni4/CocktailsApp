/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.StockAggregate;
using CocktailsApp.Domain.UserAggregate;

/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.SeedWork
{
    /// <summary>
    /// Represents the configuration for the <see cref="Stock"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Stock"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Stock"/> entity</param>
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            // PK
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.Unit);

            // Value object
            builder.ComplexProperty(s => s.Ingredient, a =>
            {
                a.IsRequired();
                a.Property(i => i.Name);
            });

            // FK
            // -- SotckTransaction
            builder.HasMany(s => s.StockTransactions)
                .WithOne()
                .HasForeignKey("StockId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
