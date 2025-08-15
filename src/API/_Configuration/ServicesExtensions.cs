using CocktailsApp.API.Assets;
using CocktailsApp.API.Auth;
using CocktailsApp.API.Common;
using CocktailsApp.API.Swagger;
using CocktailsApp.Application.Auth;
using CocktailsApp.Application.Common;

using Hellang.Middleware.ProblemDetails;

using Microsoft.Extensions.FileProviders;


namespace CocktailsApp.API.Configuration
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration cfg)
        {
            // Options binding (host-only)
            services.AddOptionsBinding(cfg); // see OptionsBinding.cs below

            // AuthN/Z
            services.AddAuth(cfg);
            services.AddAuthorization();

            services.AddControllers();
            services.AddControllers(options =>
            {
                options.Filters.Add<IdempotencyKeyFilter>();
            });
            services.AddEndpointsApiExplorer();
            services.AddCustomCors();
            services.AddCustomErrors();
            services.AddCustomLocalization();

            // Services
            services.AddHttpContextAccessor();
            services.AddScoped<IImageUrlProvider, ImageUrlProvider>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IIdempotencyKeyService, IdempotencyKeyService>();

            // AutoMapper (API profiles only)
            var licenceKey = cfg["LuckyPenny:LicenseKey"];
            services.AddAutoMapper(a => a.LicenseKey = licenceKey, typeof(ServicesExtensions).Assembly);

            // Static files for local images
            services.AddSingleton<IFileProvider>(sp =>
            {
                var env = sp.GetRequiredService<IWebHostEnvironment>();
                return new PhysicalFileProvider(env.ContentRootPath);
            });

            // Swagger, Versioning, Health
            services.AddSwaggerSetup();
            services.AddHealthChecks();

            return services;
        }

        public static WebApplication UseWebApi(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseProblemDetails();

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();
            app.MapHealthChecks("/health");

            return app;
        }
    }
}
