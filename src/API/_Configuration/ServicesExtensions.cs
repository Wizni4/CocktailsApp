using CocktailsApp.API.Assets;
using CocktailsApp.API.Auth;
using CocktailsApp.API.Common;
using CocktailsApp.API.Swagger;
using CocktailsApp.Application.Auth;
using CocktailsApp.Application.Common;

using Microsoft.Extensions.FileProviders;


namespace CocktailsApp.API.Configuration
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<IdempotencyKeyFilter>();
            });
            services.AddEndpointsApiExplorer();
            services.AddCustomCors();
            services.AddCustomErrors();
            services.AddCustomLocalization();

            // Options binding (host-only)
            services.AddOptionsBinding(cfg); // see OptionsBinding.cs below

            // AuthN/Z
            services.AddAuth(cfg);
            services.AddAuthorization();

            // Services
            services.AddHttpContextAccessor();
            services.AddScoped<IImageUrlProvider, ImageUrlProvider>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IIdempotencyKeyService, IdempotencyKeyService>();

            // AutoMapper (API profiles only)
            services.AddAutoMapper(a => { }, typeof(ServicesExtensions).Assembly);

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

        public static IApplicationBuilder UseWebApi(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Serve wwwroot (images)
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            return app;
        }
    }
}
