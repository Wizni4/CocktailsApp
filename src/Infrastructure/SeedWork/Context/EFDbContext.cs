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
using CocktailsApp.Domain.Shared;
using CocktailsApp.Domain.StockAggregate;
using CocktailsApp.Domain.UserAggregate;

using Microsoft.EntityFrameworkCore;
using System.Transactions;

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

            //Configure the tables
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Stock>(new StockMap().Configure);
            modelBuilder.Entity<StockTransaction>(new StockTransactionMap().Configure);
            modelBuilder.Entity<Address>(new AddressMap().Configure);
            modelBuilder.Entity<Ingredient>(new IngredientMap().Configure);
            modelBuilder.Entity<Order>(new OrderMap().Configure);
            modelBuilder.Entity<OrderItem>(new OrderItemMap().Configure);
            modelBuilder.Entity<IngredientPricing>(new IngredientPricingMap().Configure);
            modelBuilder.Entity<Cocktail>(new CocktailMap().Configure);
            modelBuilder.Entity<CocktailIngredient>(new CocktailIngredientMap().Configure);
            modelBuilder.Entity<Club>(new ClubMap().Configure);
            modelBuilder.Entity<ClubCocktail>(new ClubCocktailMap().Configure);
            modelBuilder.Entity<ClubMember>(new ClubMemberMap().Configure);
            modelBuilder.Entity<ClubRole>(new ClubRoleMap().Configure);
            modelBuilder.Entity<ClubPermissionEntity>(new ClubPermissionMap().Configure);
        }
    }
}
