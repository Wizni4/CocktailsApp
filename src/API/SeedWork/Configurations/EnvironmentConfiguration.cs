// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.Authentication;
using CocktailsApp.API.Club;
using CocktailsApp.API.Search;

namespace CocktailsApp.API.SeedWork
{
    public static class EnvironmentConfiguration
    {
        public static WebApplicationBuilder ConfigureEnvironment(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                 builder.Services.ConfigureOptions<CognitoJwtBearerConfiguration>();
                 builder.Services.ConfigureOptions<ClubSettingsConfiguration>();
                 builder.Services.ConfigureOptions<SearchSettingsConfiguration>();
                 builder.Services.AddLocalDbContext(builder.Configuration);
                 builder.Services.AddCognitoAuthServices(builder.Configuration);
            }

            return builder;
        }       
    }
}
