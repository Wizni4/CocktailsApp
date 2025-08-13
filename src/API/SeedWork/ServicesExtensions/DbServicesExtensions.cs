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

            // -- Write DB
            string localDbWriteConnectionStr = configuration.GetConnectionString("LocalWrite") ?? throw new ArgumentNullException("The local write DB configuration is null");
            services.AddDbContextPool<EFWriteDbContext>(options => options.UseSqlServer(localDbWriteConnectionStr));

            // -- Read DB
            string localDbReadConnectionStr = configuration.GetConnectionString("LocalRead") ?? throw new ArgumentNullException("The local read DB configuration is null");
            services.AddDbContextPool<EFReadDbContext>(options => options.UseSqlServer(localDbReadConnectionStr));

            return services;
        }

    }
}
