// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Cocktails;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.CocktailAggregate
{
    public class CocktailReadMap : IEntityTypeConfiguration<CocktailRead>
    {
        public void Configure(EntityTypeBuilder<CocktailRead> builder)
        {
            // PK
            builder.HasKey(c => c.Id);

            // Propeties
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Description).HasMaxLength(2000);
            builder.Property(c => c.ImageId).HasMaxLength(200);

            // FK
            builder.OwnsMany(c => c.Ingredients, ingr =>
            {
                ingr.ToJson();
                ingr.Property(i => i.CocktailIngredientId).IsRequired();
                ingr.Property(i => i.IngredientId).IsRequired();
                ingr.Property(i => i.Name).IsRequired().HasMaxLength(200);
                ingr.Property(i => i.Type).IsRequired().HasMaxLength(200);
                ingr.Property(i => i.IsAlcoholic).IsRequired();
                ingr.Property(i => i.ImageId).HasMaxLength(200);
                ingr.Property(i => i.Allergens);
                ingr.Property(i => i.Quantity).HasPrecision(18, 4).IsRequired();
                ingr.Property(i => i.Unit).IsRequired();
            });
        }
    }
}
