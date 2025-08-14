// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Domain.Ingredients;
using CocktailsApp.Infrastructure.SeedWork;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsApp.Infrastructure.IngredientAggregate
{
    /// <summary>
    /// Represents the configuration for the <see cref="Ingredient"/> entity to define its mapping and behavior in the database
    /// </summary>
    public class IngredientMap : EntityMap<Ingredient>
    {
        /// <summary>
        /// Configures the entity of type <see cref="Ingredient"/>
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the <see cref="Ingredient"/> entity</param>
        public override void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            base.Configure(builder);

            // Properties
            builder.Property(i => i.Name)
                .IsRequired();
            builder.Property(i => i.Type)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(i => i.IsAlcoholic)
                .IsRequired();

            // Value object
            builder.OwnsMany(i => i.Allergens, opt =>
            {
                opt.Property(a => a.Name);
            });
        }

    }
}
