// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;

namespace CocktailsApp.Application.User
{
    public class UserApplicationMapperProfile : Profile
    {
        public UserApplicationMapperProfile()
        {
            CreateMap<DomainUser, UserDTO>();
        }
    }
}
