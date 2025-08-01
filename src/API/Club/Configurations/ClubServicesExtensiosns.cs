// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.Club;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Infrastructure.ClubAggregate;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using DomainClubCocktail = CocktailsApp.Domain.ClubAggregate.ClubCocktail;
using DomainClubMember = CocktailsApp.Domain.ClubAggregate.ClubMember;
using DomainClubRole = CocktailsApp.Domain.ClubAggregate.ClubRole;


namespace CocktailsApp.API.Club
{
    public static class CocktailServicesExtensiosns
    {
        public static IServiceCollection AddClubRepositories(this IServiceCollection services)
        {
            ;
            services.AddScoped<IRepository<DomainClub>, ClubRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            return services;
        }

        public static IServiceCollection AddClubApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClubService, ClubService>();
            return services;
        }
    }
}
