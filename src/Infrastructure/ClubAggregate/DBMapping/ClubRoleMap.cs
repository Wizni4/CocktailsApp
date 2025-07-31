/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;

/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.ClubAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="ClubRole"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubRoleMap : IEntityTypeConfiguration<ClubRole>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ClubRole"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="ClubRole"/> entity</param>
        public void Configure(EntityTypeBuilder<ClubRole> builder)
        {
            // PK
            builder.HasKey(cr => cr.Id);

            // Properties
            builder.Property(cr => cr.Name);
            builder.Property(cr => cr.IsOwnerRole);
            builder.Property(cc => cc.CreationDate);
            builder.Property(cc => cc.UpdateDate);

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
