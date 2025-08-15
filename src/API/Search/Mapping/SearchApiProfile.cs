using AutoMapper;

using CocktailsApp.Application.Common;
using CocktailsApp.Application.Search;


namespace CocktailsApp.API.Search
{
    public class SearchApiProfile : Profile
    {
        public SearchApiProfile()
        {
            CreateMap<SearchResultItem, SearchItemResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}
