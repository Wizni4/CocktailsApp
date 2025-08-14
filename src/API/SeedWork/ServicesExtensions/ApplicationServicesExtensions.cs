/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

using CocktailsApp.API.Authentication;
using CocktailsApp.API.Club;
using CocktailsApp.API.Cocktail;
using CocktailsApp.API.Ingredient;
using CocktailsApp.API.IngredientPricing;
using CocktailsApp.API.Order;
using CocktailsApp.API.Search;
using CocktailsApp.API.Shared;
using CocktailsApp.API.Stock;
using CocktailsApp.API.User;
using CocktailsApp.Application.Auth;
using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.Cocktails;
using CocktailsApp.Application.Common;
using CocktailsApp.Application.Ingredients;
using CocktailsApp.Application.Orders;
using CocktailsApp.Application.Prices;
using CocktailsApp.Application.Search;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.Stock;
using CocktailsApp.Application.Users;
using CocktailsApp.Domain.Common;
using CocktailsApp.Infrastructure.ClubAggregate;
/*
 * Infrastructure namespaces
 */
using CocktailsApp.Infrastructure.SeedWork;
using CocktailsApp.Infrastructure.UserAggregate;

using FluentValidation;
/*
 * Framework namespaces
 */
using MediatR;

using System.Net;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;


namespace CocktailsApp.API.SeedWork
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // -- Club
            services.AddClubRepositories();

            // -- Cocktail
            services.AddCocktailRepositories();

            // -- Ingredient
            services.AddIngredientRepositories();

            // -- IngredientPricing
            services.AddIngredientPricingRepositories();

            // -- Order
            services.AddOrderRepositories();

            // -- Stock
            services.AddStockRepositories();

            // -- User
            services.AddUserRepositories();

            // -- SeedWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IIncludable<>), typeof(Includable<>));

            return services;
        }

        public static IServiceCollection AddReaders(this IServiceCollection services)
        {
            // -- Club
            services.AddClubReaders();

            // -- Cocktail
            services.AddCocktailReaders();

            // -- Ingredient
            services.AddIngredientReaders();

            // -- IngredientPricing
            services.AddIngredientPricingReaders();

            // -- Order
            services.AddOrderReaders();

            // -- Stock
            services.AddStockReaders();

            // -- User
            services.AddUserReaders();

            return services;
        }

        public static IServiceCollection AddProjectors(this IServiceCollection services)
        {
            // -- Club
            services.AddClubProjectors();

            // -- Cocktail
            services.AddCocktailProjectors();

            // -- Ingredient
            services.AddIngredientProjectors();

            // -- IngredientPricing
            services.AddIngredientPricingProjectors();

            // -- Order
            services.AddOrderProjectors();

            // -- Stock
            services.AddStockProjectors();

            // -- User
            services.AddUserProjectors();

            services.AddScoped<ProjectionBus>();
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            // Infra mapper
            services.AddAutoMapper(
                typeof(ClubInfraProfile),
                typeof(UserInfraProfile));

            return services;
        }

        public static IServiceCollection AddMediatR(this IServiceCollection services, IConfiguration configuration)
        {
            var licenceKey = configuration["LuckyPenny:LicenseKey"];
            services.AddMediatR(cfg =>
            {
                cfg.LicenseKey = licenceKey;

                // -- Auth
                cfg.RegisterServicesFromAssembly(typeof(AuthDTO).Assembly);

                // -- Club
                cfg.RegisterServicesFromAssembly(typeof(ClubDTO).Assembly);

                // -- Cocktail
                cfg.RegisterServicesFromAssembly(typeof(CocktailDTO).Assembly);

                // -- Ingredient
                cfg.RegisterServicesFromAssembly(typeof(IngredientDTO).Assembly);

                // -- IngredientPricing
                cfg.RegisterServicesFromAssembly(typeof(IngredientPricingDTO).Assembly);

                // -- Order
                cfg.RegisterServicesFromAssembly(typeof(OrderDTO).Assembly);

                // -- Stock
                cfg.RegisterServicesFromAssembly(typeof(StockDTO).Assembly);

                // -- Search
                cfg.RegisterServicesFromAssembly(typeof(SearchResultItem).Assembly);

                // -- User
                cfg.RegisterServicesFromAssembly(typeof(UserDTO).Assembly);

                // The projector bridge lives in Infra; events live in Domain
                cfg.RegisterServicesFromAssembly(typeof(Projector<>).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(IDomainEvent).Assembly);
            });

            return services;
        }

        public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
        {
            // -- Auth
            services.AddValidatorsFromAssembly(typeof(AuthDTO).Assembly);

            // -- Club
            services.AddValidatorsFromAssembly(typeof(ClubDTO).Assembly);

            // -- Cocktail
            services.AddValidatorsFromAssembly(typeof(CocktailDTO).Assembly);

            // -- Ingredient
            services.AddValidatorsFromAssembly(typeof(IngredientDTO).Assembly);

            // -- IngredientPricing
            services.AddValidatorsFromAssembly(typeof(IngredientPricingDTO).Assembly);

            // -- Order
            services.AddValidatorsFromAssembly(typeof(OrderDTO).Assembly);

            // -- Stock
            services.AddValidatorsFromAssembly(typeof(StockDTO).Assembly);

            // -- Search
            services.AddValidatorsFromAssembly(typeof(SearchResultItem).Assembly);

            // -- User
            services.AddValidatorsFromAssembly(typeof(UserDTO).Assembly);

            // -- SeedWork
            // Pipeline behaviour
            // Execution:
            // Idempotency → Validation → Authorization → Caching → Transaction → Handler

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // -- Club
            services.AddClubApplicationServices();

            // -- Cocktail
            services.AddCocktailApplicationServices();

            // -- Ingredient
            services.AddIngredientApplicationServices();

            // -- IngredientPricing
            services.AddIngredientPricingApplicationServices();

            // -- Order
            services.AddOrderApplicationServices();

            // -- Search
            services.AddSearchApplicationServices();

            // -- Shared
            services.AddSharedApplicationServices();

            // -- Stock
            services.AddStockApplicationServices();

            // -- User
            services.AddUserApplicationServices();

            return services;
        }

        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            // Background outbox dispatcher
            services.AddHostedService<OutboxDispatcher>();

            return services;
        }
    }
}
