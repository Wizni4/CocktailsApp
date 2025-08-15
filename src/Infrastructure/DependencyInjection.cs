using Amazon.CognitoIdentityProvider;
using CocktailsApp.Application.Auth;
using CocktailsApp.Application.Common;
using CocktailsApp.Infrastructure.Auth;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Configuration;
using CocktailsApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;


namespace CocktailsApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
        {
            // Bind options
            services.AddOptionsBinding(cfg);

            // -- Write DB
            services.AddSingleton<OutboxInterceptor>();
            string localDbWriteConnectionStr = cfg.GetConnectionString("LocalWrite") 
                ?? throw new ArgumentNullException("The local write DB configuration is null");
            services.AddDbContextPool<EFWriteDbContext>((sp,options) => 
            {
                options.UseSqlServer(localDbWriteConnectionStr);
                options.AddInterceptors(sp.GetRequiredService<OutboxInterceptor>());
            });

            // -- Repositories
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.Scan(scan => scan
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // -- Services
            services.AddScoped<IImageService, LocalImageService>();
            services.AddScoped(typeof(IIncludable<>), typeof(Includable<>));
            services.Scan(scan => scan
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(c => c.AssignableTo(typeof(IIncludesService<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // - Auth
            var authProvider = cfg.GetSection("Auth:Provider").Value?.ToLowerInvariant() ?? "memory";
            if (authProvider == "cognito")
            {
                services.AddScoped<IAuthService, CognitoAuthService>();
                services.AddAWSService<IAmazonCognitoIdentityProvider>();
            }

            // -- Redis
            var cacheProvider = cfg.GetSection("Cache:Provider").Value?.ToLowerInvariant() ?? "memory";
            var idempotencyProvider = cfg.GetSection("Idempotency:Provider").Value?.ToLowerInvariant() ?? "memory";
            if (cacheProvider == "redis" || idempotencyProvider == "redis")
            {
                var redisConfig = cfg.GetSection("Redis:Configuration").Get<string>() 
                    ?? throw new ArgumentException("Missing redis configuration");
                services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfig));
            }

            // -- Cache
            if (cacheProvider == "redis")
                services.AddSingleton<ICacheService, RedisCacheAdapter>();

            // -- Idempotency
            if (idempotencyProvider == "redis")
                services.AddSingleton<IIdempotencyService, RedisIdempotencyService>();

            // -- Kafka Producer
            services.AddSingleton<IKafkaProducer, OutboxKafkaProducer>();
            services.AddHostedService<OutboxDispatcher>();

            return services;
        }
    }
}
