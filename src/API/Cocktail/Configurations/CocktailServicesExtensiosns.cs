// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Infrastructure.CocktailAggregate;

using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;


namespace CocktailsApp.API.Cocktail
{
    public static class CocktailServicesExtensiosns
    {
        public static IServiceCollection AddCocktailRepositories(this IServiceCollection services)
        {
            ;
            services.AddScoped<IRepository<DomainCocktail>, CocktailRepository>();
            services.AddScoped<ICocktailRepository, CocktailRepository>();
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
