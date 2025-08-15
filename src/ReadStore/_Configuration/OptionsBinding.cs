

using CocktailsApp.ReadStore.Projections;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailsApp.ReadStore.Configuration
{
    public static class OptionsBinding
    {
        public static IServiceCollection AddOptionsBinding(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddOptions<KafkaOptions>().Bind(cfg.GetSection("Kafka")).ValidateOnStart();
            return services;
        }
    }
}
