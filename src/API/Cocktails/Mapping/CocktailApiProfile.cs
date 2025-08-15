using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Cocktails;
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Common;


namespace CocktailsApp.API.Cocktails
{
    public class CocktailApiProfile : Profile
    {
        public CocktailApiProfile()
        {
            // Request
            CreateMap<CreateCocktailRequest, CreateCocktailCommand>();
            CreateMap<CocktailIngredientRequest, CocktailIngredientModel>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ToEnum<UnitOfMeasure>()));

            // Response
            // -- List item
            CreateMap<CocktailListItem, CocktailListItemResponse>();

            // -- Details
            CreateMap<CocktailDetails, CocktailDetailsResponse>();
            CreateMap<CocktailIngredientView, CocktailIngredientResponse>();
        }
    }
}
