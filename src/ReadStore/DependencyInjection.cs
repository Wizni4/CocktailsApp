// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.Cocktails;
using CocktailsApp.Application.Ingredients;
using CocktailsApp.Application.Search;
using CocktailsApp.ReadStore.Clubs;
using CocktailsApp.ReadStore.Cocktails;
using CocktailsApp.ReadStore.Configuration;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Ingredients;
using CocktailsApp.ReadStore.Projections;
using CocktailsApp.ReadStore.Search;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CocktailsApp.ReadStore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReadStore(this IServiceCollection services, IConfiguration cfg)
        {
            // Bind options
            services.AddOptionsBinding(cfg);

            // Read DB
            string localDbReadConnectionStr = cfg.GetConnectionString("LocalRead") ?? throw new ArgumentNullException("The local read DB configuration is null");
            services.AddDbContextPool<EFReadDbContext>(options => options.UseSqlServer(localDbReadConnectionStr));

            // Queries
            services.AddScoped<IClubQueries, ClubQueries>();
            services.AddScoped<ICocktailQueries, CocktailQueries>();
            services.AddScoped<IIngredientQueries, IngredientQueries>();

            // Access
            services.AddScoped<IClubAccess,  ClubAccess>();

            // Services
            services.AddScoped<ISearchIndexer,  SearchIndexer>();
            services.AddScoped<ISearchService,  SearchService>();

            

            // Projections
            services.AddSingleton<IEventNameResolver>(_ =>
                new ReflectionEventNameResolver(
                    assemblies: [typeof(CocktailsApp.Domain.Common.DomainEvent).Assembly],
                    namespacePrefix: "CocktailsApp.Domain.",
                    eventMarkerInterface: typeof(CocktailsApp.Domain.Common.IDomainEvent)
            ));
            services.AddScoped<ProjectionDispatcher>();
            services.Scan(scan => scan
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(c => c.AssignableTo(typeof(IProjectionHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Kafka
            services.AddHostedService<KafkaProjectionConsumer>();

            return services;
        }
    }
}
