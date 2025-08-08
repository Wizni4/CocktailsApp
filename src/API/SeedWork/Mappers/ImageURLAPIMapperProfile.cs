// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

namespace CocktailsApp.API.SeedWork
{
    public abstract class ImageURLAPIMapperProfile : Profile
    {
        public IMappingExpression<TSource, TDestination> CreateImageMap<TSource, TDestination>() where TSource : EntityDTO where TDestination : ImageResponse
        {
            return base.CreateMap<TSource, TDestination>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageURLResolver<TSource, TDestination>>());
        }

        public IMappingExpression<TSource, TDestination> CreateImageMap<TSource, TDestination>(MemberList memberList) where TSource : EntityDTO where TDestination : ImageResponse
        {
            return base.CreateMap<TSource, TDestination>(memberList)
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageURLResolver<TSource, TDestination>>());
        }
    }
}
