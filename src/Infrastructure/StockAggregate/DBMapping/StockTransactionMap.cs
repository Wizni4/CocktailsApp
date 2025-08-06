/*
 * Domain namespaces
 */
using CocktailsApp.Domain.StockAggregate;
using CocktailsApp.Infrastructure.SeedWork;


/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.StockAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="StockTransaction"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class StockTransactionMap : EntityMap<StockTransaction>
    {
        /// <summary>
        /// Configures the entity of type <see cref="StockTransaction"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="StockTransaction"/> entity</param>
        public override void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(st => st.Date)
                .IsRequired();
            builder.Property(st => st.Description)
                .IsRequired();
            builder.Property(st => st.Quantity)
                .HasPrecision(18, 4)
                .IsRequired();
            builder.Property(st => st.TransactionType)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
