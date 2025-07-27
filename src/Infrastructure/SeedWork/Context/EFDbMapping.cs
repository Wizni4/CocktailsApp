/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.IngredientPricingAggregate;
using CocktailsApp.Domain.OrderAggregate;
using CocktailsApp.Domain.Shared;
using CocktailsApp.Domain.StockAggregate;
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
            builder.HasKey(u => u.Id);
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="Stock"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Stock"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Stock"/> entity</param>
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Unit);
            builder.HasOne(s => s.Ingredient)
                .WithMany()
                .HasForeignKey("IngredientId");
            builder.HasMany(s => s.StockTransactions)
                .WithOne()
                .HasForeignKey("StockId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="StockTransaction"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class StockTransactionMap : IEntityTypeConfiguration<StockTransaction>
    {
        /// <summary>
        /// Configures the entity of type <see cref="StockTransaction"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="StockTransaction"/> entity</param>
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            builder.HasKey(st => st.Id);
            builder.Property(st => st.Date);
            builder.Property(st => st.Description);
            builder.Property(st => st.Quantity);
            builder.Property(st => st.TransactionType);
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="Address"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Address"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Address"/> entity</param>
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property<Guid>("Id").IsRequired();
            builder.HasKey("Id");
            builder.Property(a => a.Street).IsRequired();
            builder.Property(a => a.StreetNumber).IsRequired();
            builder.Property(a => a.City).IsRequired();
            builder.Property(a => a.PostalCode).IsRequired();
            builder.Property(a => a.State).IsRequired();
            builder.Property(a => a.Country).IsRequired();
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="Ingredient"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class IngredientMap : IEntityTypeConfiguration<Ingredient>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Ingredient"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Ingredient"/> entity</param>
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property<Guid>("Id").IsRequired();
            builder.HasKey("Id");
            builder.Property(i => i.Name).IsRequired();
        }
    }

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
            builder.HasKey(o => o.Id);
            builder.HasOne<User>().WithMany().HasForeignKey(o => o.CustomerId);
            builder.HasOne<Club>().WithMany().HasForeignKey(o => o.ClubId);
            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(o => o.OrderDate);
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="OrderItem"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        /// <summary>
        /// Configures the entity of type <see cref="OrderItem"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="OrderItem"/> entity</param>
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.HasOne<Cocktail>()
                .WithMany()
                .HasForeignKey(oi => oi.CocktailId);
            builder.Property(oi => oi.Quantity);
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="IngredientPricing"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class IngredientPricingMap : IEntityTypeConfiguration<IngredientPricing>
    {
        /// <summary>
        /// Configures the entity of type <see cref="IngredientPricing"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="IngredientPricing"/> entity</param>
        public void Configure(EntityTypeBuilder<IngredientPricing> builder)
        {
            builder.HasKey(ip => ip.Id);
            builder.Property(ip => ip.Cost);
            builder.HasOne(ip => ip.Ingredient)
                .WithMany()
                .HasForeignKey("IngredientId");
            builder.Property(ip => ip.Cost);
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="Cocktail"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class CocktailMap : IEntityTypeConfiguration<Cocktail>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Cocktail"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Cocktail"/> entity</param>
        public void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany<CocktailIngredient>()
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "CocktailToIngredient",
                    j => j.HasOne<CocktailIngredient>()
                        .WithMany()
                        .HasForeignKey("CocktailIngredientId"),
                    j => j
                        .HasOne<Cocktail>()
                        .WithMany()
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("CocktailIngredientId", "CocktailId");
                    });
            builder.Property(c => c.Name);
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="CocktailIngredient"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class CocktailIngredientMap : IEntityTypeConfiguration<CocktailIngredient>
    {
        /// <summary>
        /// Configures the entity of type <see cref="CocktailIngredient"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="CocktailIngredient"/> entity</param>
        public void Configure(EntityTypeBuilder<CocktailIngredient> builder)
        {
            builder.Property<Guid>("Id").IsRequired();
            builder.HasKey("Id");
            builder.HasOne(ci => ci.Ingredient)
                .WithMany()
                .HasForeignKey("IngredientId");
            builder.Property(ci => ci.Quantity).IsRequired();
        }
    }

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
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Address)
                .WithMany()
                .HasForeignKey("AddressId");
            builder.HasMany(c => c.Cocktails)
                .WithOne()
                .HasForeignKey("ClubId")
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(c => c.Description);
            builder.Property(c => c.Name);
            builder.HasMany(c => c.Members)
                .WithOne()
                .HasForeignKey("ClubId")
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Owner)
                .WithOne()
                .HasForeignKey<ClubMember>("OwnerId")
                //.HasForeignKey("OwnerId")
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.Roles)
                .WithOne()
                .HasForeignKey("ClubId")
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(c => c.Visibility);
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="ClubCocktail"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubCocktailMap : IEntityTypeConfiguration<ClubCocktail>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ClubCocktail"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="ClubCocktail"/> entity</param>
        public void Configure(EntityTypeBuilder<ClubCocktail> builder)
        {
            builder.HasKey(cc => cc.Id);
            builder.HasOne<Cocktail>()
                .WithMany()
                .HasForeignKey(cc => cc.CocktailId);
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="ClubMember"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubMemberMap : IEntityTypeConfiguration<ClubMember>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ClubMember"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="ClubMember"/> entity</param>
        public void Configure(EntityTypeBuilder<ClubMember> builder)
        {
            builder.HasKey(cm => cm.Id);
            builder.HasMany<ClubPermissionEntity>()
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "ClubMemberToClubPermission",
                    j => j.HasOne<ClubPermissionEntity>()
                        .WithMany()
                        .HasForeignKey("PermissionId"),
                    j => j
                        .HasOne<ClubMember>()
                        .WithMany()
                        .HasForeignKey("ClubMemberId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("ClubMemberId", "PermissionId");
                    });
            builder.HasMany(cm => cm.Roles);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(cm => cm.UserId);

        }
    }

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
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Name);
            builder.HasMany<ClubPermissionEntity>()
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "ClubRoleToClubPermission",
                    j => j.HasOne<ClubPermissionEntity>()
                        .WithMany()
                        .HasForeignKey("PermissionId"),
                    j => j
                        .HasOne<ClubRole>()
                        .WithMany()
                        .HasForeignKey("ClubRoleId"),
                    j =>
                    {
                        j.HasKey("ClubRoleId", "PermissionId");
                    });
        }
    }

    /// <summary>
    /// Represents the configuration for the <see cref="ClubPermission"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class ClubPermissionMap : IEntityTypeConfiguration<ClubPermissionEntity>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ClubPermissionEntity"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="ClubPermissionEntity"/> entity</param>
        public void Configure(EntityTypeBuilder<ClubPermissionEntity> builder)
        {
            builder.Property(cp => cp.Id).IsRequired();
            builder.Property(cp => cp.Name).IsRequired();
        }
    }
}
