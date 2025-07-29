/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.UserAggregate;

/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.SeedWork
{
    /// <summary>
    /// Represents the configuration for the <see cref="Club"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubMap : IEntityTypeConfiguration<Club>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Club"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Club"/> entity</param>
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            // PK
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.Description);
            builder.Property(c => c.Name);
            builder.Property(c => c.Visibility);

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

            // -- ClubMember (Owner)
            builder.HasOne(c => c.Owner)
                .WithOne()
                .HasForeignKey<ClubMember>("OwnerId")
                .OnDelete(DeleteBehavior.Restrict);

            // -- ClubRole
            builder.HasMany(c => c.Roles)
                .WithOne()
                .HasForeignKey("ClubId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
