// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.Cocktails;
using CocktailsApp.Infrastructure.CocktailAggregate;
using CocktailsApp.Infrastructure.SeedWork;

using System.Security.Cryptography;


namespace CocktailsApp.API.Cocktails
{
    public static class CocktailServicesExtensiosns
    {
        public static IServiceCollection AddCocktailRepositories(this IServiceCollection services)
        {
            ;
            services.AddScoped<ICocktailRepository, CocktailRepository>();
            return services;
        }

        public static IServiceCollection AddCocktailReaders(this IServiceCollection services)
        {
            ;
            services.AddScoped<ICocktailReader, CocktailReader>();
            return services;
        }

        public static IServiceCollection AddCocktailProjectors(this IServiceCollection services)
        {
            services.AddScoped<IProjection, CocktailProjection>();
            services.AddScoped<IProjection, CocktailCrossAggregateProjection>();
            return services;
        }

        public static IServiceCollection AddCocktailApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICocktailService, CocktailService>();
            services.AddScoped<ICocktailsSearchService, CocktailSearchService>();
            return services;
        }
    }
}
