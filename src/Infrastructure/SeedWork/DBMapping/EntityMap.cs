// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.SeedWork
{
    public abstract class EntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // PK
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.CreationDate)
                .IsRequired();
            builder.Property(e => e.CreatedBy)
                .IsRequired();
            builder.Property(e => e.UpdateDate)
                .IsRequired();
            builder.Property(e => e.UpdatedBy)
                .IsRequired();
            builder.Property(e => e.ImageId);
        }
    }
}
