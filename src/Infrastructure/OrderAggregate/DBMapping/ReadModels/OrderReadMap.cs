// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.Order;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.OrderAggregate
{
    public class OrderReadMap : IEntityTypeConfiguration<OrderRead>
    {
        public void Configure(EntityTypeBuilder<OrderRead> builder)
        {
            // PK
            builder.HasKey(o => o.OrderId);

            // Properties
            builder.Property(o => o.ClubId).IsRequired();
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.Username).IsRequired().HasMaxLength(200);
            builder.Property(o => o.OrderDate).IsRequired();

            // FK
            builder.OwnsMany(o => o.Items, itm =>
            {
                itm.ToJson();

                itm.Property(oi => oi.CocktailId).IsRequired();
                itm.Property(oi => oi.Name).IsRequired().HasMaxLength(200);
                itm.Property(oi => oi.Descritpion).HasMaxLength(2000);
                itm.Property(oi => oi.ImageId).HasMaxLength(200);
                itm.Property(oi => oi.Quantity).IsRequired().HasPrecision(18, 4);
            });
        }
    }
}
