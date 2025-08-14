// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Prices;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.IngredientPricingAggregate
{
    public class IngredientPricingReadMap : IEntityTypeConfiguration<IngredientPricingRead>
    {
        public void Configure(EntityTypeBuilder<IngredientPricingRead> builder)
        {
            // PK
            builder.HasKey(ip => ip.IngredientPricingId);

            // Properties
            builder.Property(ip => ip.IngredientId).IsRequired();
            builder.Property(ip => ip.Name).IsRequired().HasMaxLength(200);
            builder.Property(ip => ip.Type).IsRequired().HasMaxLength(200);
            builder.Property(ip => ip.IsAlcoholic).IsRequired();
            builder.Property(ip => ip.ImageId).HasMaxLength(200);
            builder.Property(ip => ip.Cost).HasPrecision(18, 4).IsRequired();
            builder.Property(ip => ip.Price).HasPrecision(18, 4).IsRequired();
            builder.Property(ip => ip.Margin).HasPrecision(18, 4).IsRequired();
        }
    }
}
