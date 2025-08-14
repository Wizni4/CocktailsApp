// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Stocks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.StockAggregate
{
    public class StockReadMap : IEntityTypeConfiguration<StockRead>
    {
        public void Configure(EntityTypeBuilder<StockRead> builder)
        {
            // PK
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.ClubId).IsRequired();
            builder.Property(s => s.IngredientId).IsRequired();
            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Type).IsRequired().HasMaxLength(200);
            builder.Property(s => s.IsAlcoholic).IsRequired();
            builder.Property(s => s.ImageId).HasMaxLength(200);
            builder.Property(s => s.Quantity).IsRequired().HasPrecision(18, 4);
            builder.Property(s => s.Unit).IsRequired().HasMaxLength(200);

            // FK
            builder.OwnsMany(s => s.Transactions, trs =>
            {
                trs.ToJson();

                trs.Property(t => t.Id).IsRequired();
                trs.Property(t => t.Date).IsRequired();
                trs.Property(t => t.Description).IsRequired().HasMaxLength(2000);
                trs.Property(t => t.Quantity).IsRequired().HasPrecision(18, 4);
                trs.Property(t => t.TransactionType).IsRequired().HasMaxLength(200);
            });
        }
    }
}
