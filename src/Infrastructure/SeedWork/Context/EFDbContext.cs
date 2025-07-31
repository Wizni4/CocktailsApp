/*
 * Domain namespaces
 */

/*
* Framework namespaces
*/
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.IngredientPricingAggregate;
using CocktailsApp.Domain.OrderAggregate;
using CocktailsApp.Domain.StockAggregate;
using CocktailsApp.Domain.UserAggregate;
using CocktailsApp.Infrastructure.ClubAggregate;
using CocktailsApp.Infrastructure.CocktailAggregate;
using CocktailsApp.Infrastructure.IngredientPricingAggregate;
using CocktailsApp.Infrastructure.OrderAggregate;
using CocktailsApp.Infrastructure.StockAggregate;
using CocktailsApp.Infrastructure.UserAggregate;

using Microsoft.EntityFrameworkCore;


namespace CocktailsApp.Infrastructure.SeedWork
{
    /// <summary>
    /// Represents the database context for interacting with the underlying database using Entity Framework.
    /// </summary>
    /// <remarks>
    /// This DbContext class encapsulates the database connection and provides access to database tables/entities
    /// through DbSet properties. It enables querying, inserting, updating, and deleting entities from the database.
    /// </remarks>
    public class EFDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EFDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext for configuration.</param>
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.MapClubAggregate()
                .MapCocktailAggregate()
                .MapIngredientPricingAggregate()
                .MapOrderAggregate()
                .MapStockAggregate()
                .MapUserAggregate();
        }
    }

    public static class ModelBuilderExtension
    {
        public static ModelBuilder MapClubAggregate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>(new ClubMap().Configure)
                .Entity<ClubCocktail>(new ClubCocktailMap().Configure)
                .Entity<ClubMember>(new ClubMemberMap().Configure)
                .Entity<ClubRole>(new ClubRoleMap().Configure);

            return modelBuilder;
        }

        public static ModelBuilder MapCocktailAggregate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cocktail>(new CocktailMap().Configure);

            return modelBuilder;
        }

        public static ModelBuilder MapIngredientPricingAggregate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientPricing>(new IngredientPricingMap().Configure);

            return modelBuilder;
        }

        public static ModelBuilder MapOrderAggregate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(new OrderMap().Configure)
                .Entity<OrderItem>(new OrderItemMap().Configure);

            return modelBuilder;
        }

        public static ModelBuilder MapStockAggregate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>(new StockMap().Configure)
                .Entity<StockTransaction>(new StockTransactionMap().Configure);

            return modelBuilder;
        }

        public static ModelBuilder MapUserAggregate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(new UserMap().Configure);

            return modelBuilder;
        }        
    }
}
