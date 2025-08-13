// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.User;
using CocktailsApp.Infrastructure.SeedWork;
using CocktailsApp.Infrastructure.UserAggregate;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;

namespace CocktailsApp.API.User
{
    public static class UserServicesExtensiosns
    {
        public static IServiceCollection AddUserRepositories(this IServiceCollection services)
        {
            ;
            services.AddScoped<IRepository<DomainUser>, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddUserReaders(this IServiceCollection services)
        {
            services.AddScoped<IUserReader, UserReader>();
            return services;
        }

        public static IServiceCollection AddUserProjectors(this IServiceCollection services)
        {
            services.AddScoped<IProjection, UserProjection>();
            services.AddScoped<IProjection, UserCrossAggregateProjection>();
            return services;
        }

        public static IServiceCollection AddUserApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
