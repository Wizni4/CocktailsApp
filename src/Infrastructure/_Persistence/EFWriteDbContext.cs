using CocktailsApp.Domain.Clubs;
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Domain.Ingredients;
using CocktailsApp.Domain.Orders;
using CocktailsApp.Domain.Prices;
using CocktailsApp.Domain.Stocks;
using CocktailsApp.Domain.Users;

using Microsoft.EntityFrameworkCore;


namespace CocktailsApp.Infrastructure.Persistence
{
    /// <summary>
    /// Represents the database context for interacting with the underlying database using Entity Framework.
    /// </summary>
    /// <remarks>
    /// This DbContext class encapsulates the database connection and provides access to database tables/entities
    /// through DbSet properties. It enables querying, inserting, updating, and deleting entities from the database.
    /// </remarks>
    public class EFWriteDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EFWriteDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext for configuration.</param>
        public EFWriteDbContext(DbContextOptions<EFWriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.MapClubAggregate()
                .MapCocktailAggregate()
                .MapIngredient()
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
            modelBuilder.Entity<Club>(new ClubConfiguration().Configure)
                .Entity<ClubCocktail>(new ClubCocktailConfiguration().Configure)
                .Entity<ClubMember>(new ClubMemberConfiguration().Configure)
                .Entity<ClubRole>(new ClubRoleConfiguration().Configure);

            return modelBuilder;
        }

        public static ModelBuilder MapCocktailAggregate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cocktail>(new CocktailConfiguration().Configure);
            modelBuilder.Entity<CocktailIngredient>(new CocktailIngredientConfiguration().Configure);

            return modelBuilder;
        }

        public static ModelBuilder MapIngredient(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(new IngredientConfiguration().Configure);

            return modelBuilder;
        }

        public static ModelBuilder MapIngredientPricingAggregate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pricing>(new PricingConfiguration().Configure);

            return modelBuilder;
        }

        public static ModelBuilder MapOrderAggregate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(new OrderConfiguration().Configure);

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
            modelBuilder.Entity<User>(new UserConfiguration().Configure);

            return modelBuilder;
        }
    }
}
