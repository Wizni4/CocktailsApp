// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Clubs;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.ClubAggregate
{
    public class ClubReadMap : IEntityTypeConfiguration<ClubRead>
    {
        public void Configure(EntityTypeBuilder<ClubRead> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Visibility).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.Property(x => x.ImageId).HasMaxLength(200);

            // Address (owned, JSON)
            builder.OwnsOne(x => x.Address, addr =>
            {
                addr.Property(a => a.Street).HasMaxLength(200);
                addr.Property(a => a.StreetNumber).HasMaxLength(50);
                addr.Property(a => a.City).HasMaxLength(120);
                addr.Property(a => a.PostalCode).HasMaxLength(30);
                addr.Property(a => a.State).HasMaxLength(120);
                addr.Property(a => a.Country).HasMaxLength(120);
            });

            // Members (owned collection, JSON)
            builder.OwnsMany(x => x.Members, mem =>
            {
                mem.Property(m => m.Username).IsRequired().HasMaxLength(120);
                // Nested roles for each member (JSON)
                mem.OwnsMany(m => m.Roles, r =>
                {
                    r.Property(x => x.Name).IsRequired().HasMaxLength(120);
                    // Permissions is a List<string> → sits inside JSON as an array (no converter needed)
                });
            });

            // Club-level roles (if you use them at this level too)
            builder.OwnsMany(x => x.Roles, r =>
            {
                r.Property(x => x.Name).IsRequired().HasMaxLength(120);
            });

            // Cocktails (owned collection, JSON)
            builder.OwnsMany(x => x.Cocktails, c =>
            {
                c.Property(k => k.Name).IsRequired().HasMaxLength(200);
                c.Property(k => k.Description).HasMaxLength(2000);
                c.Property(k => k.ImageId).HasMaxLength(200);

                // Ingredients per cocktail (JSON)
                c.OwnsMany(k => k.Ingredients, i =>
                {
                    i.Property(x => x.Name).IsRequired().HasMaxLength(200);
                    i.Property(x => x.Type).HasMaxLength(120);
                    i.Property(x => x.Unit).HasMaxLength(30);
                    i.Property(x => x.ImageId).HasMaxLength(200);
                    i.Property(x => x.IsAlcoholic).IsRequired();
                    i.Property(x => x.Quantity).HasPrecision(18, 3);
                    // Allergens is List<string> → stays as JSON array
                });
            });
        }
    }
}
