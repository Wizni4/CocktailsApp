/*
 * Domain namespaces
 */

/*
* Framework namespaces
*/



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
        }
    }
}
