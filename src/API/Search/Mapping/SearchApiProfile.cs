using AutoMapper;

using CocktailsApp.Application.Common;
using CocktailsApp.Application.Search;


namespace CocktailsApp.API.Search
{
    public class SearchApiProfile : Profile
    {
        public SearchApiProfile(IImageUrlProvider images)
        {
            CreateMap<SearchResultItem, SearchItemResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(Enum.Parse<ImageSubject>(src.Type.ToString()), src.ImageId, ImageVariant.Small)));
        }
    }
}
