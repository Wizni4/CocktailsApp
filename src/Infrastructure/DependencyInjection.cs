using CocktailsApp.Application.Common;
using CocktailsApp.Infrastructure.Auth;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using Newtonsoft.Json.Serialization;

using StackExchange.Redis;


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


            // -- Redis Cache
            var cacheProvider = configuration.GetSection("Cache")["Provider"]?.ToLowerInvariant() ?? "memory";
            if (cacheProvider == "redis")
            {
                var cs = configuration.GetSection("Cache:Redis:Configuration").Get<string>() ?? throw new ArgumentException("Missing redis configuration");
                var ns = configuration.GetSection("Cache:Redis:Instance").Get<string>() ?? throw new ArgumentException("Missing redis configuration");

                services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(cs));
                services.AddSingleton<ICacheService>(sp =>
                {
                    var mux = sp.GetRequiredService<IConnectionMultiplexer>();
                    return new RedisCacheAdapter(mux, ns);
                });
            }

            // -- Idempotency
            services.AddOptions<IdempotencyOptions>()
                .Bind(configuration.GetSection("Idempotency"))
                .ValidateOnStart();

            var idempotencyProvider = configuration.GetSection("Cache")["Provider"]?.ToLowerInvariant() ?? "memory";
            if (idempotencyProvider == "redis")
            {
                var cs = configuration["Cache:Redis:Configuration"] ?? "localhost:6379";
                var ns = configuration["Idempotency:Namespace"] ?? "yourapp:idem:";

                services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(cs));
                services.AddSingleton(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DateParseHandling = DateParseHandling.DateTimeOffset,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    Formatting = Formatting.None
                });

                services.AddSingleton<IIdempotencyService>(sp =>
                {
                    var mux = sp.GetRequiredService<IConnectionMultiplexer>();
                    var opts = sp.GetRequiredService<IOptions<IdempotencyOptions>>();
                    return new RedisIdempotencyService(mux, ns, opts);
                });
            }

            return services;
        }
    }
}
