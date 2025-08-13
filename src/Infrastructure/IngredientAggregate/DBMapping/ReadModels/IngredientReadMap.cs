// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Ingredient;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Text.Json;


namespace CocktailsApp.Infrastructure.IngredientAggregate
{
    public class IngredientReadMap : IEntityTypeConfiguration<IngredientRead>
    {
        private static readonly JsonSerializerOptions s_jsonOpts = new(JsonSerializerDefaults.Web);
        public void Configure(EntityTypeBuilder<IngredientRead> builder)
        {
            // PK
            builder.HasKey(i => i.Id);

            // Properties
            builder.Property(i => i.Name).IsRequired().HasMaxLength(200);
            builder.Property(i => i.Type).IsRequired().HasMaxLength(200);
            builder.Property(i => i.IsAlcoholic).IsRequired();
            builder.Property(i => i.ImageId).HasMaxLength(200);
            builder.Property(i => i.Allergens)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, s_jsonOpts),
                    v => JsonSerializer.Deserialize<List<string>>(v, s_jsonOpts)!);
        }
    }
}
