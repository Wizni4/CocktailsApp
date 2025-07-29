/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.OrderAggregate;
using CocktailsApp.Domain.UserAggregate;
/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.SeedWork
{
    /// <summary>
    /// Represents the configuration for the <see cref="Order"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Order"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Order"/> entity</param>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // PK
            builder.HasKey(o => o.Id);

            // Properties
            builder.Property(o => o.OrderDate);

            // FK
            // -- User
            builder.HasOne<User>().WithMany().HasForeignKey(o => o.CustomerId);

            // -- Club
            builder.HasOne<Club>().WithMany().HasForeignKey(o => o.ClubId);

            // -- OrderItem
            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
