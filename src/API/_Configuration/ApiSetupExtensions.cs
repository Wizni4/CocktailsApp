using Amazon.CognitoIdentityProvider.Model;

using FluentValidation;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

using System.Globalization;

namespace CocktailsApp.API.Configuration
{
    public static class ApiSetupExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomErrors(this IServiceCollection services)
        {
            services.AddProblemDetails(options =>
            {
                // Show exception details (stack traces) only in Development
                options.IncludeExceptionDetails = (ctx, ex) =>
                    ctx.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment();  // 🔒 Never include stack traces

                // 1) Make Hellang log any unhandled exception (500+) with the exception object
                options.ShouldLogUnhandledException = (http, ex, details) => true;

                options.Map<ValidationException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = ex.Message
                    });

                options.Map<ArgumentException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = ex.Message
                    });

                options.Map<NotAuthorizedException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Unauthorized",
                        Status = StatusCodes.Status401Unauthorized,
                        Detail = ex.Message
                    });

                options.Map<KeyNotFoundException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Not Found",
                        Status = StatusCodes.Status404NotFound,
                        Detail = ex.Message
                    });

                options.Map<UnauthorizedAccessException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Forbidden",
                        Status = StatusCodes.Status403Forbidden,
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
    }
}
