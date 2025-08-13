// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.Club;
using CocktailsApp.Infrastructure.ClubAggregate;
using CocktailsApp.Infrastructure.SeedWork;
using CocktailsApp.Infrastructure.Shared;


namespace CocktailsApp.API.Club
{
    public static class ClubServicesExtensiosns
    {
        public static IServiceCollection AddClubRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClubRepository, ClubRepository>();
            return services;
        }

        public static IServiceCollection AddClubReaders(this IServiceCollection services)
        {
            ;
            services.AddScoped<IRepository<DomainClub>, ClubRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            return services;
        }

        public static IServiceCollection AddClubApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IClubSearchService, ClubSearchService>();
            return services;
        }
    }
}
