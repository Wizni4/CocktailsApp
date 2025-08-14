using AutoMapper;

using CocktailsApp.Application.Common;


namespace CocktailsApp.API.Common
{
    public class CommonApiMapping : Profile
    {
        public CommonApiMapping()
        {
            CreateMap(typeof(PagedResult<>), typeof(PagedResponse<>));
        }
    }
}
