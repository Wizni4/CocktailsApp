
using CocktailsApp.Infrastructure.Auth;
using CocktailsApp.Infrastructure.Common;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailsApp.Infrastructure.Configuration
{
    public static class OptionsBinding
    {
        public static IServiceCollection AddOptionsBinding(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddOptions<CognitoOptions>().Bind(cfg.GetSection("AWS:Cognito")).ValidateOnStart();
            services.AddOptions<ImageStorageOptions>().Bind(cfg.GetSection("Images:Storage")).ValidateOnStart();
            services.AddOptions<CacheOptions>().Bind(cfg.GetSection("Cache")).ValidateOnStart();
            services.AddOptions<IdempotencyOptions>().Bind(cfg.GetSection("Idempotency")).ValidateOnStart();
            services.AddOptions<KafkaOptions>().Bind(cfg.GetSection("Kafka")).ValidateOnStart();
            return services;
        }
    }
}
