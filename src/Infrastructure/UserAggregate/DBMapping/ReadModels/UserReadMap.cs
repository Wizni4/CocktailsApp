// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.User;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailsApp.Infrastructure.UserAggregate
{
    public class UserReadMap : IEntityTypeConfiguration<UserRead>
    {
        public void Configure(EntityTypeBuilder<UserRead> builder)
        {
            // PK
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.Username).IsRequired().HasMaxLength(200);
            builder.Property(u => u.ImageId).HasMaxLength(200);
        }
    }
}
