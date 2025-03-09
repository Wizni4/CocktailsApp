/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.Cocktails;
using CocktailsApp.Application.Shared;
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.Shared;

namespace CocktailsApp.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Cocktail
            CreateMap<Cocktail, CocktailDTO>();
            CreateMap<CocktailIngredient, CocktailIngredientDTO>();

            // Shared
            CreateMap<Ingredient, IngredientDTO>();
        }
    }
}
