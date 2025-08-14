using CocktailsApp.Infrastructure.Auth;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CocktailsApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CognitoOptions>(configuration.GetSection("AWS:Cognito"));
            services.AddOptions<ImageStorageOptions>().Bind(configuration.GetSection("Image:Storage")).ValidateOnStart();

            // -- Write DB
            string localDbWriteConnectionStr = configuration.GetConnectionString("LocalWrite") ?? throw new ArgumentNullException("The local write DB configuration is null");
            services.AddDbContextPool<EFWriteDbContext>(options => options.UseSqlServer(localDbWriteConnectionStr));

            return services;
        }
    }
}
