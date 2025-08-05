

using CocktailsApp.Application.Search;

namespace CocktailsApp.API.Search
{
    public static class SearchServicesExtensions
    {


        public static IServiceCollection AddSearchApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<Application.Search.ISearchService, SearchService>();
            return services;
        }
    }
}
