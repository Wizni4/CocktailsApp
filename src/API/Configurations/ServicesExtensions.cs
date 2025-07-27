/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.Club;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.User;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;
/*
 * Infrastructure namespaces
 */
using CocktailsApp.Infrastructure.ClubAggregate;
using CocktailsApp.Infrastructure.SeedWork;
using CocktailsApp.Infrastructure.UserAggregate;

using FluentValidation;
/*
 * Framework namespaces
 */
using Hellang.Middleware.ProblemDetails;

using MediatR;

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using System.Globalization;


namespace CocktailsApp.API
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
#if LOCAL
            string connectionStr = configuration.GetConnectionString("Local");
#elif AWS_TEST
            string connectionStr = await SecretManager.GetSecret(configuration);
#endif
            services.AddDbContextPool<EFDbContext>(options => options.UseSqlServer(connectionStr));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IIncludable<>), typeof(Includable<>));
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Club>, ClubRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClubService, ClubService>();
            return services;
        }

        public static IServiceCollection AddApplicationHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateClubCommandHandler>());
            services.AddValidatorsFromAssemblyContaining<CreateClubCommandValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
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

        public static IServiceCollection AddCustomErrors(this IServiceCollection services)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => false; // 🔒 Never include stack traces

                options.Map<ValidationException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = ex.Message
                    });

                options.Map<KeyNotFoundException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Not Found",
                        Status = StatusCodes.Status404NotFound,
                        Detail = ex.Message
                    });
            });
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
                c.EnableAnnotations();
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
