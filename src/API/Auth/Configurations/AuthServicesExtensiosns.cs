// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Amazon.CognitoIdentityProvider;

using CocktailsApp.Application.Auth;
using CocktailsApp.Infrastructure.Authentication;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.API.Authentication
{
    public static class AuthServicesExtensiosns
    {
        public static IServiceCollection AddCognitoAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.Configure<CognitoSettings>(configuration.GetSection("AWS:Cognito"));
            services.AddScoped<IAuthService, CognitoAuthService>();
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonCognitoIdentityProvider>();
            services.AddSingleton<ICookieService, CookieService>();

            return services;
        }
    }
}
