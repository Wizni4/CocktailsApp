/*
 * Domain namespaces
 */

/*
* Framework namespaces
*/

using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.Cocktails;
using CocktailsApp.Application.Ingredients;
using CocktailsApp.Application.Prices;
using CocktailsApp.Application.Orders;
using CocktailsApp.Application.Stock;
using CocktailsApp.Application.Users;
using CocktailsApp.Infrastructure.ClubAggregate;
using CocktailsApp.Infrastructure.CocktailAggregate;
using CocktailsApp.Infrastructure.IngredientAggregate;
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
    public class EFReadDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EFWriteDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext for configuration.</param>
        public EFReadDbContext(DbContextOptions<EFReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClubRead>(new ClubReadMap().Configure);
            modelBuilder.Entity<CocktailRead>(new CocktailReadMap().Configure);
            modelBuilder.Entity<IngredientRead>(new IngredientReadMap().Configure);
            modelBuilder.Entity<IngredientPricingRead>(new IngredientPricingReadMap().Configure);
            modelBuilder.Entity<OrderRead>(new OrderReadMap().Configure);
            modelBuilder.Entity<StockRead>(new StockReadMap().Configure);
            modelBuilder.Entity<UserRead>(new UserReadMap().Configure);
        }
    }
}
