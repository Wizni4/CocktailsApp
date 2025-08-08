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
using CocktailsApp.API.Search;
using CocktailsApp.API.Shared;
using CocktailsApp.API.User;
using CocktailsApp.Application.Authentication;
using CocktailsApp.Application.Club;
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.Search;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.User;

/*
 * Infrastructure namespaces
 */
using CocktailsApp.Infrastructure.SeedWork;

using FluentValidation;


/*
 * Framework namespaces
 */
using MediatR;


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

            // -- User
            services.AddUserRepositories();

            // -- SeedWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IIncludable<>), typeof(Includable<>));

            return services;
        }

        public static IServiceCollection AddEventDispatcher(this IServiceCollection services)
        {
            // -- SeedWork
            services.AddScoped<DomainEventDispatcher>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            // Application mapper
            services.AddAutoMapper(
                typeof(ClubApplicationMapperProfile),
                typeof(CocktailApplicationMapperProfile),
                typeof(IngredientApplicationMapperProfile),
                typeof(SearchApplicationMapperProfile),
                typeof(SharedApplicationMapperProfile),
                typeof(UserApplicationMapperProfile));

            // API mapper
            services.AddAutoMapper(
                typeof(AuthAPIMapperProfile),
                typeof(ClubAPIMapperProfile),
                typeof(CocktailAPIMapperProfile),
                typeof(IngredientAPIMapperProfile),
                typeof(SharedAPIMapperProfile),
                typeof(UserAPIMapperProfile));

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

                // -- Search
                cfg.RegisterServicesFromAssembly(typeof(GlobalSearchResultDTO).Assembly);

                // -- User
                cfg.RegisterServicesFromAssembly(typeof(UserDTO).Assembly);
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

            // -- Search
            services.AddValidatorsFromAssembly(typeof(GlobalSearchResultDTO).Assembly);

            // -- User
            services.AddValidatorsFromAssembly(typeof(UserDTO).Assembly);

            // -- SeedWork
            // Pipeline behaviour
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

            // -- Search
            services.AddSearchApplicationServices();

            // -- Shared
            services.AddSharedApplicationServices();

            // -- User
            services.AddUserApplicationServices();

            return services;
        }
    }
}
