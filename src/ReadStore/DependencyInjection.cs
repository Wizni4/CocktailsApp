// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.ReadStore.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CocktailsApp.ReadStore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReadStore(this IServiceCollection services, IConfiguration configuration)
        {
            // -- Read DB
            string localDbReadConnectionStr = configuration.GetConnectionString("LocalRead") ?? throw new ArgumentNullException("The local read DB configuration is null");
            services.AddDbContextPool<EFReadDbContext>(options => options.UseSqlServer(localDbReadConnectionStr));
            return services;
        }
    }
}
