using CocktailsApp.Domain.Users;
using CocktailsApp.Infrastructure.Common;

using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.Persistence
{
    /// <summary>
    /// Represents the configuration for the <see cref="User"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class UserConfiguration : EntityConfiguration<User>
    {
        /// <summary>
        /// Configures the entity of type <see cref="User"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the User entity.</param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(u => u.Username)
                .IsRequired();
            builder.Property(u => u.Email)
                .IsRequired();
#if LOCAL
            builder.Property(u => u.Password);
#else
            builder.Ignore(u => u.Password);
#endif
        }
    }
}
