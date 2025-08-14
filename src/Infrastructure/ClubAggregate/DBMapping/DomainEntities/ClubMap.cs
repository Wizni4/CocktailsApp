/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Infrastructure.SeedWork;


/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.ClubAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="Club"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubMap : EntityMap<Club>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Club"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Club"/> entity</param>
        public override void Configure(EntityTypeBuilder<Club> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(c => c.Description)
                .IsRequired();
            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c => c.Visibility)
                .HasConversion<string>()
                .IsRequired();

            // Value objects
            builder.ComplexProperty(c => c.Address, a =>
            {
                a.IsRequired();

                a.Property(a => a.Street).IsRequired();
                a.Property(a => a.StreetNumber).IsRequired();
                a.Property(a => a.City).IsRequired();
                a.Property(a => a.PostalCode).IsRequired();
                a.Property(a => a.State).IsRequired();
                a.Property(a => a.Country).IsRequired();
            });

            // FK
            // -- Address
            builder.HasMany(c => c.Cocktails)
                .WithOne()
                .HasForeignKey("ClubId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // -- ClubMember
            builder.HasMany(c => c.Members)
                .WithOne()
                .HasForeignKey("ClubId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // -- ClubRole
            builder.HasMany(c => c.Roles)
                .WithOne()
                .HasForeignKey("ClubId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
