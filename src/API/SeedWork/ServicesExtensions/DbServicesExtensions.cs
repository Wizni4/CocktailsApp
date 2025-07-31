// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Infrastructure.SeedWork;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.API.SeedWork
{
    public static class DbServicesExtensions
    {
        public static IServiceCollection AddLocalDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionStr = configuration.GetConnectionString("Local") ?? throw new ArgumentNullException("The local DB configuration is null");
            services.AddDbContextPool<EFDbContext>(options => options.UseSqlServer(connectionStr));
            return services;
        }

    }
}
