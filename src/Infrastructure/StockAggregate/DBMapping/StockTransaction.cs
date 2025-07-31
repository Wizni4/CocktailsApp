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


namespace CocktailsApp.Infrastructure.StockAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="StockTransaction"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class StockTransactionMap : IEntityTypeConfiguration<StockTransaction>
    {
        /// <summary>
        /// Configures the entity of type <see cref="StockTransaction"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="StockTransaction"/> entity</param>
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            // PK
            builder.HasKey(st => st.Id);

            // Properties
            builder.Property(st => st.Date);
            builder.Property(st => st.Description);
            builder.Property(st => st.Quantity).HasPrecision(18, 4);
            builder.Property(st => st.TransactionType);
            builder.Property(cc => cc.CreationDate);
            builder.Property(cc => cc.UpdateDate);
        }
    }
}
