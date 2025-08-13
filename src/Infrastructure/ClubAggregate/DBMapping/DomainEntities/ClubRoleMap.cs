/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Infrastructure.SeedWork;


/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.ClubAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="ClubRole"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubRoleMap : EntityMap<ClubRole>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ClubRole"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="ClubRole"/> entity</param>
        public override void Configure(EntityTypeBuilder<ClubRole> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(cr => cr.Name)
                .IsRequired();
            builder.Property(cr => cr.IsOwnerRole)
                .IsRequired();

            // Value objects
            builder.OwnsMany(cm => cm.Permissions, a =>
            {
                a.Property(p => p.Permission)
                    .HasConversion<string>()
                    .IsRequired();
            });
        }
    }
}
