/*
 * Domain namespaces
 */

/*
* Framework namespaces
*/



using CocktailsApp.ReadStore.Clubs;
using CocktailsApp.ReadStore.Cocktails;
using CocktailsApp.ReadStore.Common;
using CocktailsApp.ReadStore.Ingredients;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Users;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Context
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

            // Domain events
            modelBuilder.Entity<ProcessedEvent>(new ProcessedEventConfiguration().Configure);

            // -- Club
            modelBuilder.Entity<ClubRead>(new ClubReadConfiguration().Configure);
            modelBuilder.Entity<ClubCocktailRead>(new ClubCocktailReadConfiguration().Configure);
            modelBuilder.Entity<ClubMemberRead>(new ClubMemberReadConfiguration().Configure);
            modelBuilder.Entity<ClubMemberRoleRead>(new ClubMemberRoleReadConfiguration().Configure);
            modelBuilder.Entity<ClubRoleRead>(new ClubRoleReadConfiguration().Configure);
            modelBuilder.Entity<ClubRolePermissionRead>(new ClubRolePermissionReadConfiguration().Configure);

            // -- Cocktail
            modelBuilder.Entity<CocktailRead>(new CocktailReadConfiguration().Configure);
            modelBuilder.Entity<CocktailIngredientRead>(new CocktailIngredientReadConfiguration().Configure);

            // -- Ingredients
            modelBuilder.Entity<AllergenRead>(new AllergenReadConfiguration().Configure);
            modelBuilder.Entity<IngredientRead>(new IngredientReadConfiguration().Configure);

            // -- User
            modelBuilder.Entity<UserSummaryRead>(new UserSummaryConfiguration().Configure);

        }
    }
}
