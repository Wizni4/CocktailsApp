/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Domain.Ingredients;
using CocktailsApp.Domain.Stocks;
using CocktailsApp.Domain.Users;
using CocktailsApp.Infrastructure.SeedWork;


/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.StockAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="Stock"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class StockMap : EntityMap<Stock>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Stock"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Stock"/> entity</param>
        public override void Configure(EntityTypeBuilder<Stock> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(s => s.Unit)
                .HasConversion<string>()
                .IsRequired();

            // FK
            // -- Ingredient
            builder.HasOne<Ingredient>()
                .WithMany()
                .HasForeignKey(s => s.IngredientId)
                .IsRequired();

            // -- SotckTransaction
            builder.HasMany(s => s.StockTransactions)
                .WithOne()
                .HasForeignKey("StockId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
