// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Users;
using CocktailsApp.Domain.Users;


namespace CocktailsApp.Infrastructure.UserAggregate
{
    public class UserInfraProfile : Profile
    {
        public UserInfraProfile()
        {
            CreateMap<UserCreatedEvent, UserRead>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
