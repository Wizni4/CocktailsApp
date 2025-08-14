using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Common;
using CocktailsApp.Application.Ingredients;
using CocktailsApp.Domain.Ingredients;


namespace CocktailsApp.API.Ingredients
{
    public class IngredientApiProfile : Profile
    {
        public IngredientApiProfile(IImageUrlProvider images)
        {
            // Response
            CreateMap<IngredientDetails, IngredientDetailsResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToEnum<IngredientType>()))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(ImageSubject.Ingredient, src.ImageId, ImageVariant.Small)));
        }
    }
}
