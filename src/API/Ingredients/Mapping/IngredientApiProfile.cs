using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Common;
using CocktailsApp.Application.Ingredients;
using CocktailsApp.Domain.Ingredients;


namespace CocktailsApp.API.Ingredients
{
    public class IngredientApiProfile : Profile
    {
        public IngredientApiProfile()
        {
            // Response
            CreateMap<IngredientDetails, IngredientDetailsResponse>();
        }
    }
}
