/*
 * Domain namespaces
 */
using CocktailsApp.Domain.UserAggregate;

/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.SeedWork
{
    /// <summary>
    /// Represents the configuration for the <see cref="User"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class UserMap : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the entity of type <see cref="User"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the User entity.</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // PK
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.Username);
            builder.Property(u => u.Email);
#if LOCAL
            builder.Property(u => u.Password);
#else
            builder.Ignore(u => u.Password);
#endif
        }
    }
}
