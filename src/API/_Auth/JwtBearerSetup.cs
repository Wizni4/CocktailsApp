using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CocktailsApp.API.Auth
{
    public static class JwtBearerSetup
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration cfg)
        {
            // Base auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            var provider = cfg.GetSection("Auth")["Provider"]?.ToLowerInvariant();

            if (provider == "cognito")
            {
                services.ConfigureOptions<CognitoJwtBearerConfiguration>();
            }
            else // default to local
            {
                services.ConfigureOptions<LocalJwtBearerConfiguration>();
            }

            return services;
        }
    }
}
