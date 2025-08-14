using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Cocktails;
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Common;


namespace CocktailsApp.API.Cocktails
{
    public class CocktailApiProfile : Profile
    {
        public CocktailApiProfile(IImageUrlProvider images)
        {
            // Request
            CreateMap<CreateCocktailRequest, CreateCocktailCommand>();
            CreateMap<CocktailIngredientRequest, CocktailIngredientModel>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ToEnum<UnitOfMeasure>()));

            // Response
            // -- List item
            CreateMap<CocktailListItem, CocktailListItemResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(ImageSubject.Club, src.ImageId, ImageVariant.Small)));

            // -- Details
            CreateMap<CocktailDetails, CocktailDetailsResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(ImageSubject.Club, src.ImageId, ImageVariant.Small)));
            CreateMap<CocktailIngredientView, CocktailIngredientResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(ImageSubject.Club, src.ImageId, ImageVariant.Small)));
        }
    }
}
