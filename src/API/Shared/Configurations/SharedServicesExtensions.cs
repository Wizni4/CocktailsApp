// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Shared;
using CocktailsApp.Infrastructure.SeedWork;
using CocktailsApp.Infrastructure.Shared;

namespace CocktailsApp.API.Shared
{
    public static class SharedServicesExtensions
    {
        public static IServiceCollection AddSharedApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, LocalImageService>();
            return services;
        }
    }
}
