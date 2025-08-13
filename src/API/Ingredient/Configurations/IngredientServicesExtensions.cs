// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.Club;
using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Infrastructure.IngredientAggregate;
using CocktailsApp.Infrastructure.SeedWork;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;


namespace CocktailsApp.API.Ingredient
{
    public static class IngredientServicesExtensions
    {
        public static IServiceCollection AddIngredientRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<DomainIngredient>, IngredientRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            return services;
        }

        public static IServiceCollection AddIngredientReaders(this IServiceCollection services)
        {
            ;
            services.AddScoped<IIngredientReader, IngredientReader>();
            return services;
        }

        public static IServiceCollection AddIngredientProjectors(this IServiceCollection services)
        {
            services.AddScoped<IProjection, IngredientProjection>();
            services.AddScoped<IProjection, IngredientCrossAggregateProjection>();
            return services;
        }

        public static IServiceCollection AddIngredientApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IIngredientSearchService, IngredientSearchService>();
            return services;
        }
    }
}
