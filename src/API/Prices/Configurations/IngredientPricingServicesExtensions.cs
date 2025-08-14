// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Prices;
using CocktailsApp.Infrastructure.IngredientPricingAggregate;
using CocktailsApp.Infrastructure.SeedWork;

namespace CocktailsApp.API.Prices
{
    public static class IngredientPricingServicesExtensions
    {
        public static IServiceCollection AddIngredientPricingRepositories(this IServiceCollection services)
        {
            services.AddScoped<PricingRepository, IngredientPricingRepository>();
            return services;
        }

        public static IServiceCollection AddIngredientPricingReaders(this IServiceCollection services)
        {
            services.AddScoped<IIngredientPricingReader, IngredientPricingReader>();
            return services;
        }

        public static IServiceCollection AddIngredientPricingProjectors(this IServiceCollection services)
        {
            services.AddScoped<IProjection, IngredientPricingProjection>();
            services.AddScoped<IProjection, IngredientPricingCrossAggregateProjection>();
            return services;
        }

        public static IServiceCollection AddIngredientPricingApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
