// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using CocktailsApp.Application.Stock;
using CocktailsApp.Infrastructure.SeedWork;
using CocktailsApp.Infrastructure.Shared;
using CocktailsApp.Infrastructure.StockAggregate;

namespace CocktailsApp.API.Stock
{
    public static class StockServicesExtensions
    {
        public static IServiceCollection AddStockRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStockRepository, StockRepository>();
            return services;
        }

        public static IServiceCollection AddStockReaders(this IServiceCollection services)
        {
            ;
            services.AddScoped<IStockReader, StockReader>();
            return services;
        }

        public static IServiceCollection AddStockProjectors(this IServiceCollection services)
        {
            services.AddScoped<IProjection, StockProjection>();
            services.AddScoped<IProjection, StockCrossAggregateProjection>();
            return services;
        }

        public static IServiceCollection AddStockApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
