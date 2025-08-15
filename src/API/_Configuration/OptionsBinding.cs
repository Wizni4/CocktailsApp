
using CocktailsApp.API.Assets;
using CocktailsApp.Application.Common;
using CocktailsApp.Application.Search;

namespace CocktailsApp.API.Configuration
{
    public static class OptionsBinding
    {
        public static IServiceCollection AddOptionsBinding(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddOptions<SearchOptions>().Bind(cfg.GetSection("Search")).ValidateOnStart();
            services.AddOptions<PageOptions>().Bind(cfg.GetSection("PageOptions")).ValidateOnStart();
            services.AddOptions<ImageOptions>().Bind(cfg.GetSection("Images:Url")).ValidateOnStart();
            return services;
        }
    }
}
