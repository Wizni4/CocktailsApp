// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using DomainUser = CocktailsApp.Domain.Users.User;

namespace CocktailsApp.Application.Users
{
    public class UserDTOProfile : Profile
    {
        public UserDTOProfile()
        {
            CreateMap<UserRead, UserDTO>();
        }
    }
}
