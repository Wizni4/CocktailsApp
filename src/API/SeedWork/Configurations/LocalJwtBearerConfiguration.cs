// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace CocktailsApp.API.SeedWork
{
    public class LocalJwtBearerConfiguration : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IConfiguration _configuration;

        public LocalJwtBearerConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

#pragma warning disable CS8767
        public void Configure(string name, JwtBearerOptions options)
#pragma warning restore CS8767
        {
            Configure(options);
        }

        public void Configure(JwtBearerOptions options)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = true,
                ValidAudience = audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            // Optional: Add logging for easier debugging
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"AUTH FAILED: {context.Exception.Message}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("TOKEN VALIDATED");
                    return Task.CompletedTask;
                }
            };
        }
    }
}
