/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;
/*
 * Application namespaces
 */
/*
 * Infrastructure namespaces
 */
using CocktailsApp.Infrastructure.SeedWork;
/*
 * Framework namespaces
 */
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;


namespace CocktailsApp.API
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            return services;
        }

        public static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
        {
            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-US");

                var cultures = new CultureInfo[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("fr-FR"),
                };

                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Define the BearerAuth scheme
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                // Apply the BearerAuth scheme globally to all operations
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }
    }
}
