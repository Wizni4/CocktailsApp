/*
 * Application namespaces
 */
using Application.Cocktails;
using Application.Shared;
/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Domain namespaces
 */
using Domain.CocktailAggregate;
using Domain.Shared;

namespace Application.Mappers
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
