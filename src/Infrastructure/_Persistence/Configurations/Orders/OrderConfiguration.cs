using CocktailsApp.Domain.Clubs;
using CocktailsApp.Domain.Orders;
using CocktailsApp.Domain.Users;
using CocktailsApp.Infrastructure.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.Persistence
{
    /// <summary>
    /// Represents the configuration for the <see cref="Order"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Order"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Order"/> entity</param>
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(o => o.OrderDate)
                .IsRequired();

            // Value Object
            builder.OwnsMany(o => o.Items);

            // FK
            // -- User
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            // -- Club
            builder.HasOne<Club>()
                .WithMany()
                .HasForeignKey(o => o.ClubId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
